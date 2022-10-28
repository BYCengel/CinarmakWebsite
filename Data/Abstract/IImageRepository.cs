using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IImageRepository : IRepository<Image>
    {
        Task<List<Image>> GetHomeDisplayImagesAsync();

        void DeleteProductImages(int productId);
        void DeleteProductImage(int productId, int imageId);
        void RemoveImageFromProduct(int productId, string imageUrl);

        void Update(Image entity);

    }
}
