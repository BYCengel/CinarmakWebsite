using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICategoryService : IValidator<Category>
    {
        /// <summary>
        /// Returns a category with the given Id Asyncly, 
        /// </summary>
        /// <param name="id">Id of the wanted category</param>
        /// <returns>A category</returns>
        Task<Category> GetByIdAsync(int id);

        /// <summary>
        /// Returns a category with the given Id
        /// </summary>
        /// <param name="id">Id of the wanted category</param>
        /// <returns></returns>
        Category GetById(int id);

        /// <summary>
        /// Returns a category together with associated Products with the given Id Asyncly.
        /// </summary>
        /// <param name="id">Id of the wanted category</param>
        /// <returns>A category with its products</returns>
        Task<Category> GetByIdWithProductsAsync(int id);

        /// <summary>
        /// Returns all categories Asyncly
        /// </summary>
        /// <returns>A list of categories</returns>
        Task<List<Category>> GetAllAsync();

        /// <summary>
        /// Adds the given category to database.
        /// </summary>
        /// <param name="entity">Category to add</param>
        void Create(Category entity);
        /// <summary>
        /// Switches the given entity with its placeholder in database using its Id.
        /// </summary>
        /// <param name="entity">Category to update</param>
        void Update(Category entity);

        /// <summary>
        /// Deletes the given entity in database using its Id.
        /// </summary>
        /// <param name="entity">Category to delete.</param>
        void Delete(Category entity);

        /// <summary>
        /// Deletes the ProductCategory associated with given ids.
        /// </summary>
        /// <param name="productId">Id of the product</param>
        /// <param name="categoryId">Id of the category</param>
        void RemoveFromCategory(int productId, int categoryId);

        /// <summary>
        /// Increases ViewCount by 1 for the category associated with categoryUrl.
        /// </summary>
        /// <param name="categoryUrl">categoryUrl of the desired category.</param>
        void IncreaseCategoryViewCount(string categoryUrl);
    }
}
