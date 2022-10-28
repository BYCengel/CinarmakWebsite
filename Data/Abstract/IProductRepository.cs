using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IProductRepository : IRepository<Product>
    {
        /// <summary>
        /// returns a list of products async which are related to the given categoryName, page and pageSize.
        /// </summary>
        /// <param name="categoryName">name of category for query</param>
        /// <param name="pageSize">number of products in a page</param>
        /// <param name="page">wanted page (set of products)</param>
        /// <returns>a list of products</returns>
        Task<List<Product>> GetProductsByCategoryAsync(string categoryUrl, int pageSize, int page, bool isAdmin);

        /// <summary>
        /// returns a list of products async for home page display.
        /// </summary>
        /// <returns>a list of products</returns>
        Task<List<Product>> GetHomePageProductsAsync();

        /// <summary>
        /// gets related products using the searchString async.
        /// </summary>
        /// <param name="searchString">query string</param>
        /// <returns>a list of products</returns>
        Task<List<Product>> GetSearchResultsAsync(string searchString);

        /// <summary>
        /// gets popular products for homepage display
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetPopularProductsAsync();

        /// <summary>
        /// returns all the popular products.
        /// </summary>
        /// <returns></returns>
        List<Product> GetPopularProducts();

        /// <summary>
        /// gets suggested products for homepage display
        /// </summary>
        /// <returns></returns>
        Task<List<Product>> GetSuggestedProductsAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortingIndex"></param>
        /// <returns></returns>
        Task<List<Product>> GetSortedProducts(int sortingIndex);

        /// <summary>
        /// Gets the images of the product associated with the productId
        /// </summary>
        /// <param name="productId"></param>
        /// <returns>Images of product</returns>
        List<Image> GetProductImages(int productId);

        /// <summary>
        /// gets a product and its categories using the products Id asyncly.
        /// </summary>
        /// <param name="id">wanted products Id</param>
        /// <returns>a product</returns>
        Task<Product> GetByIdWithCategoriesAsync(int id);

        /// <summary>
        /// gets a products details using its productUrl.
        /// </summary>
        /// <param name="productUrl">products url</param>
        /// <returns>a product</returns>
        Product GetProductDetails(string productUrl);

        /// <summary>
        /// Adds given categories to the product asyncly.
        /// </summary>
        /// <param name="productId">Id of the product which categories will be added</param>
        /// <param name="categoryIds">Ids of the categories to add</param>
        /// <returns></returns>
        Task AddCategoriesAsync(int productId, int[] categoryIds);

        /// <summary>
        /// Adds given categories to the product
        /// </summary>
        /// <param name="productId">Id of the product which categories will be added</param>
        /// <param name="categoryIds">Ids of the categories to add</param>
        void AddCategories(int productId, int[] categoryIds);

        /// <summary>
        /// Updates a given product using its Id with the new categories.
        /// </summary>
        /// <param name="entity">Changed Product</param>
        /// <param name="categoryIds">Array of changed products categories</param>
        void Update(Product entity, int[] categoryIds);

        /// <summary>
        /// Gets count of products belonging to given category
        /// if categoryUrl is null or empty; function will return count of all products.
        /// </summary>
        /// <param name="categoryUrl">wanted categories URL</param>
        /// <returns>count of products</returns>
        int GetCountByCategory(string categoryUrl);

        /// <summary>
        /// Returns count of products in database.
        /// </summary>
        /// <returns></returns>
        int GetProductsCount();

        /// <summary>
        /// returns ids of the products within range 
        /// </summary>
        /// <param name="numberOfProducts">number of products to return</param>
        /// <param name="range">how many products to select from</param>
        /// <returns></returns>
        List<Product> GetRandomPopularProducts(int numberOfProducts, int range);

        /// <summary>
        /// Assigns the popular products according to given Ids.
        /// </summary>
        /// <param name="newProductIds">Ids to add popular status</param>
        void AssignNewPopularProducts(int[] newProductIds);

        /// <summary>
        /// Increase the ViewCount by 1 for given productUrl
        /// </summary>
        /// <param name="productUrl">productUrl of the desired product</param>
        void IncreaseProductViewCount(string productUrl);

        void AddImage(int productId, int imageId);

        List<Category> GetCategoriesOfProduct(int productId);

    }
}
