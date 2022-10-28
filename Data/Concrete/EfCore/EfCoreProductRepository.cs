using Data.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class EfCoreProductRepository : EfCoreGenericRepository<Product>, IProductRepository
    {
        public EfCoreProductRepository(ShopContext context) : base(context)
        {

        }

        private ShopContext ShopContext { get { return context as ShopContext; } }

        public async Task<Product> GetByIdWithCategoriesAsync(int id)
        {
            return await ShopContext.Products
                        .Where(p => p.Id == id)
                        .Include(p => p.ProductCategories)
                        .ThenInclude(pc => pc.Category)
                        .FirstOrDefaultAsync();
        }

        public int GetCountByCategory(string categoryUrl)
        {
            var products = ShopContext.Products.Where(p => p.IsApproved).AsQueryable();

            if (!string.IsNullOrEmpty(categoryUrl))
            {
                return products.Include(p => p.ProductCategories)
                               .ThenInclude(pc => pc.Category)
                               .Where(p => p.ProductCategories.Any(pc => pc.Category.CategoryUrl == categoryUrl)).Count();
            }
            return products.Count();
        }

        public async Task<List<Product>> GetHomePageProductsAsync()
        {
            return await ShopContext.Products
                            .Where(p => p.IsSuggested && p.IsApproved).ToListAsync();
        }

        public Product GetProductDetails(string productUrl)
        {
            return ShopContext.Products
                            .Where(p => p.ProductUrl == productUrl)
                            .Include(p => p.ProductCategories)
                            .ThenInclude(pc => pc.Category)
                            .Include(p => p.ProductImages)
                            .ThenInclude(pi => pi.Image)
                            .FirstOrDefault();
        }

        public List<Image> GetProductImages(int productId)
        {
            var product = ShopContext.Products.Where(p => p.Id == productId).Include(p => p.ProductImages).ThenInclude(pi => pi.Image).FirstOrDefault();
            var images = product.ProductImages.Select(pi => pi.Image).ToList();

            return images;
        }

        public async Task<List<Product>> GetProductsByCategoryAsync(string categoryUrl, int pageSize, int page, bool isAdmin)
        {
            if (isAdmin)
            {
                var products = ShopContext.Products.AsQueryable();

                if (!string.IsNullOrEmpty(categoryUrl))
                {
                    products = products.Include(p => p.ProductCategories)
                                        .ThenInclude(pc => pc.Category)
                                        .Where(p => p.ProductCategories.Any(pc => pc.Category.CategoryUrl == categoryUrl));
                }

                return await products.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            }
            else
            {
                var products = ShopContext.Products.Where(p => p.IsApproved).AsQueryable();

                if (!string.IsNullOrEmpty(categoryUrl))
                {
                    products = products.Include(p => p.ProductCategories)
                                        .ThenInclude(pc => pc.Category)
                                        .Where(p => p.ProductCategories.Any(pc => pc.Category.CategoryUrl == categoryUrl));
                }

                return await products.Skip(pageSize * (page - 1)).Take(pageSize).ToListAsync();
            }
        }

        public async Task<List<Product>> GetSearchResultsAsync(string searchString)
        {
            var products = ShopContext.Products
                .Where(p => p.IsApproved && (p.Name.ToLower().Contains(searchString.ToLower()) || p.CardDescription.ToLower().Contains(searchString.ToLower()) || p.Description.ToLower().Contains(searchString)));

            return await products.ToListAsync();
        }

        public async Task<List<Product>> GetSuggestedProductsAsync()
        {
            List<Product> products = await ShopContext.Products.Where(p => p.IsSuggested == true && p.IsApproved == true).ToListAsync();

            return products;
        }

        public async Task<List<Product>> GetPopularProductsAsync()
        {
            List<Product> products = await ShopContext.Products.Where(p => p.IsPopular == true && p.IsApproved == true).ToListAsync();
            return products;
        }

        public void IncreaseProductViewCount(string productUrl)
        {
            Product product = ShopContext.Products
                .Where(p => p.ProductUrl == productUrl)
                .FirstOrDefault();
            product.ViewCount += 1;
        }

        public void Update(Product entity, int[] categoryIds)
        {
            Product product = ShopContext.Products
                                        .Include(p => p.ProductCategories)
                                        .FirstOrDefault(p => p.Id == entity.Id);

            if (product != null)
            {
                product.Name = entity.Name;
                product.ProductUrl = entity.ProductUrl;
                //product.ImageUrl = entity.ImageUrl;
                product.MainImageUrl = entity.MainImageUrl;
                product.Description = entity.Description;
                //product.BaseDolarPrice = entity.BaseDolarPrice;
                product.BasePrice = entity.BasePrice;
                product.SalePrice = entity.SalePrice;
                product.Quantity = entity.Quantity;
                product.IsApproved = entity.IsApproved;
                product.IsSuggested = entity.IsSuggested;
                product.IsAltered = entity.IsAltered;
                product.StockState = entity.StockState;
                product.BasePriceType = entity.BasePriceType;

                product.ProductCategories = categoryIds.Select(categoryId => new ProductCategory()
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                }).ToList();
            }
        }

        public async Task AddCategoriesAsync(int productId, int[] categoryIds)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            foreach (var categoryId in categoryIds)
            {
                productCategories.Add(new ProductCategory()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });
            }

            await ShopContext.AddRangeAsync(productCategories);
        }

        public void AddCategories(int productId, int[] categoryIds)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            foreach (var categoryId in categoryIds)
            {
                productCategories.Add(new ProductCategory()
                {
                    ProductId = productId,
                    CategoryId = categoryId
                });
            }

            ShopContext.AddRange(productCategories);
        }

        public void AddImage(int productId, int imageId)
        {
            ProductImage productImage = new ProductImage()
            {
                ProductId = productId,
                ImageId = imageId
            };

            ShopContext.Add(productImage);
        }

        public List<Category> GetCategoriesOfProduct(int productId)
        {
            List<ProductCategory> productCategories = new List<ProductCategory>();
            Product product = ShopContext.Products.Where(p => p.Id == productId).Include(p => p.ProductCategories).FirstOrDefault();
            productCategories = product.ProductCategories;
            List<Category> categories = new List<Category>();

            foreach (var pc in productCategories)
            {
                if (ShopContext.Categories.Find(pc.CategoryId) != null)
                {
                    categories.Add(ShopContext.Categories.Find(pc.CategoryId));

                }
            }
            return categories;
        }

        public Task<List<Product>> GetSortedProducts(int sortingIndex)
        {
            if (sortingIndex == 1) // alphabetical
            {
                var products = ShopContext.Products.OrderBy(p => p.Name).ToListAsync();

                return products;
            }
            else if (sortingIndex == 2) // ascending price
            {
                var products = ShopContext.Products.OrderBy(p => p.SalePrice).ToListAsync();
                return products;
            }
            else if (sortingIndex == 3) // descending price
            {
                var products = ShopContext.Products.OrderByDescending(p => p.SalePrice).ToListAsync();
                return products;
            }
            else
            {
                throw new Exception("sortingIndex not defined.");
            }
        }

        public int GetProductsCount()
        {
            return ShopContext.Products.Count();
        }

        public void AssignNewPopularProducts(int[] newProductIds)
        {
            foreach (var number in newProductIds)
            {
                Console.WriteLine("in assign new popular products async " + number);
            }

            foreach (var product in GetPopularProducts())
            {
                product.IsPopular = false;
            }

            List<Product> newProducts = ShopContext.Products.Where(p => newProductIds.Contains(p.Id)).ToList();

            foreach (var product in newProducts)
            {
                product.IsPopular = true;
            }
        }

        public List<Product> GetRandomPopularProducts(int numberOfProducts, int range)
        {
            List<Product> products = ShopContext.Products.OrderByDescending(p => p.ViewCount).Take(range).ToList();
            List<Product> productsToReturn = new List<Product>();
            Random random = new Random();

            while (true)
            {
                var temp = products[random.Next(products.Count)];
                productsToReturn.Add(temp);
                products.Remove(temp);

                if (productsToReturn.Count >= numberOfProducts) break;
            }

            return productsToReturn;
        }

        public List<Product> GetPopularProducts()
        {
            return ShopContext.Products.Where(p => p.IsPopular == true && p.IsApproved == true).ToList();
        }
    }
}
