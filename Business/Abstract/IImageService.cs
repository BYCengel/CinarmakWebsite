using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IImageService : IValidator<Image>
    {
        /// <summary>
        /// Returns the home display images Asyncly.
        /// </summary>
        /// <returns>A list of Images.</returns>
        Task<List<Image>> GetHomeDisplayImagesAsync();

        /// <summary>
        /// Adds the given Image to the database.
        /// </summary>
        /// <param name="entity">Image to add</param>
        void Create(Image entity);

        /// <summary>
        /// Switches the given Image with its placeholder in database using its Id.
        /// </summary>
        /// <param name="entity">Image to update.</param>
        void Update(Image entity);

        /// <summary>
        /// Deletes the given Image from database.
        /// </summary>
        /// <param name="entity">Image to delete.</param>
        void Delete(Image entity);

        /// <summary>
        /// Removes corresponding image from the product of given Id.
        /// </summary>
        /// <param name="productId">Id of product</param>
        /// <param name="imageUrl">Url of image</param>
        void RemoveImageFromProduct(int productId, string imageUrl);
    }
}
