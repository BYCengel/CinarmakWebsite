using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService : IValidator<Product>
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Product GetById(int id);
        Product GetProductDetails(string productUrl);
        Task<Product> GetByIdWithCategoriesAsync(int id);
        Task<List<Product>> GetProductsByCategoryAsync(string categoryUrl, int pageSize, int page, bool isAdmin);
        Task<List<Product>> GetSearchResultsAsync(string searchString);
        Task<List<Product>> GetPopularProductsAsync();
        Task<List<Product>> GetSuggestedProductsAsync();

        Task<List<Product>> GetSortedProducts(int sortingIndex);
        List<Product> GetRandomPopularProducts(int numberOfProducts, int range);

        /// <summary>
        /// Returns the categories of the Product which associated with the Id
        /// </summary>
        /// <param name="productId">Id of the desired product</param>
        /// <returns>A list of categories</returns>

        List<Category> GetCategoriesOfProduct(int productId);

        List<Image> GetProductImages(int productId);
        int GetCountByCategory(string categoryUrl);
        int GetProductsCount();
        bool Create(Product entity);
        Task AddCategoriesAsync(int productId, int[] categoryIds);
        void AddCategories(int productId, int[] categoryIds);
        void IncreaseProductViewCount(string productUrl);
        void AddImage(int productId, int imageId);
        void AssignNewPopularProducts(int[] newProductIds);
        bool Update(Product entity, int[] categoryIds);
        bool Update(Product entity);
        void Delete(Product entity);
    }
}
