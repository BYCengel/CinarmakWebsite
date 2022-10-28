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
    public class EfCoreImageRepository : EfCoreGenericRepository<Image>, IImageRepository
    {
        public EfCoreImageRepository(ShopContext context) : base(context)
        {

        }
        private ShopContext ShopContext { get { return context as ShopContext; } }

        public void DeleteProductImage(int productId, int imageId)
        {
            ProductImage piToRemove = new ProductImage();
            piToRemove.ProductId = productId;
            piToRemove.ImageId = imageId;
            context.Remove(piToRemove);
        }

        public void DeleteProductImages(int productId)
        {
            ShopContext.Remove(ShopContext.Products.Find(productId).ProductImages);
        }

        public async Task<List<Image>> GetHomeDisplayImagesAsync()
        {
            var images = await ShopContext.Images.Where(img => img.IsHomeDisplay == true).ToListAsync();
            return images;
        }

		public void RemoveImageFromProduct(int productId, string imageUrl)
		{
            var image = ShopContext.Images.Where(img => img.Url == imageUrl).FirstOrDefault();
            ProductImage pi = new ProductImage
            {
                ProductId = productId,
                ImageId = image.Id
            };

            ShopContext.Remove(pi);
		}

		public void Update(Image image, int[] productIds)
        {
            Image imageToChange = ShopContext.Images.Where(img => img.Id == image.Id).First();

            if(imageToChange != null)
            {
                imageToChange.Url = image.Url;
                imageToChange.IsMainImageOfProduct = image.IsMainImageOfProduct;
                imageToChange.IsHomeDisplay = image.IsHomeDisplay;
            }
        }
    }
}
