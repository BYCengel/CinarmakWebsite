using Business.Abstract;
using Entities;
using WebsiteUI.Models;

namespace WebsiteUI.FunctionsLibrary
{
    public class AssistantFunctions
    {
        public ProductModel CreateProductModel(Product product)
        {
            return new ProductModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                CardDescription = product.CardDescription,
                ProductUrl = GenerateUrl(product.Name),
                MainImageUrl = product.MainImageUrl,
                //ImageUrl = product.ImageUrl,
                //BaseDolarPrice = product.BaseDolarPrice,
                BasePrice = product.BasePrice,
                SalePrice = product.SalePrice,
                Quantity = product.Quantity,
                ViewCount = product.ViewCount,
                PurchaseCount = product.PurchaseCount,
                IsApproved = product.IsApproved,
                IsHomeDisplay = product.IsSuggested,
                StockStateNumber = ((int)product.StockState),
                BasePriceTypeNumber = ((int)product.BasePriceType)
            };
        }

        public List<ProductModel> CreateProductModels(List<Product> products)
        {
            List<ProductModel> models = new List<ProductModel>();

            foreach (var product in products)
            {
                models.Add(CreateProductModel(product));
            }

            return models;
        }

        public List<ProductModel> OrderProductModels(List<ProductModel> pModels, int sortingIndex)
        {
            if(sortingIndex == -1)
            {
                return pModels;
            }

            if (sortingIndex == 1) // alphabetical
            {
                return pModels.OrderBy(p => p.Name).ToList();
            }
            else if (sortingIndex == 2) // ascending price
            {
                return pModels.OrderBy(p => p.SalePrice).ToList();
            }
            else if (sortingIndex == 3) // descending price
            {
                return pModels.OrderByDescending(p => p.SalePrice).ToList();
            }
            else if (sortingIndex == 4) // descending price
            {
                return pModels.OrderByDescending(p => p.ViewCount).ToList();
            }
            else
            {
                throw new Exception("sortingIndex not defined.");
            }
        }

        public CategoryModel CreateCategoryModel(Category category)
        {
            return new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                ViewCount = category.ViewCount,
                Url = GenerateUrl(category.CategoryUrl)
            };
        }

        public Category CreateCategory(CategoryModel model)
        {
            return new Category
            {
                Name = model.Name,
                ViewCount = model.ViewCount,
                CategoryUrl = GenerateUrl(model.Name)
            };
        }

        public ImageModel CreateImageModel(Image image)
        {
            return new ImageModel
            {
                Url = image.Url,
                IsHomeDisplay = image.IsHomeDisplay,
                IsMainImageOfProduct = image.IsMainImageOfProduct
            };
        }

        public List<ImageModel> CreateImageModels(List<Image> images)
        {

            List<ImageModel> list = new List<ImageModel>();
            foreach (var image in images)
            {
                list.Add(CreateImageModel(image));
            }

            return list;
        }

        public Product CreateProduct(ProductModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                //ImageUrl = model.ImageUrl,
                MainImageUrl = model.MainImageUrl,
                CardDescription = model.CardDescription,
                Description = model.Description,
                ProductUrl = GenerateUrl(model.Name),
                //BaseDolarPrice = model.BaseDolarPrice,
                BasePrice = model.BasePrice,
                SalePrice = model.SalePrice,
                Quantity = model.Quantity,
                IsApproved = model.IsApproved,
                IsSuggested = model.IsHomeDisplay,
                IsPopular = model.IsPopular,
            };

            product.SetStockState(model.StockStateNumber);
            product.SetBasePriceType(model.BasePriceTypeNumber);

            return product;
        }

        public Image CreateImage(ImageModel imageModel)
        {
            return new Image
            {
                IsHomeDisplay = imageModel.IsHomeDisplay,
                IsMainImageOfProduct = imageModel.IsMainImageOfProduct,
                Url = imageModel.Url
            };
        }

        public string GenerateUrl(string text)
        {
            string newText = text.ToLower().Replace(" ", "-").Replace("ö", "o").Replace("ı", "i").Replace("ü", "u").Replace("ç", "c").Replace("ğ", "g");
            return newText;
        }
    }
}
