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
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {

        public EfCoreCategoryRepository(ShopContext context) : base(context)
        {

        }
        public ShopContext ShopContext
        {
            get { return context as ShopContext; }
        }

        public async Task<Category> GetByIdWithProductsAsync(int categoryId)
        {
            var category = await ShopContext.Categories
                            .Where(c => c.Id == categoryId)
                            .Include(c => c.ProductCategories)
                            .ThenInclude(pc => pc.Product)
                            .FirstOrDefaultAsync();

            return category;
        }

        public void RemoveFromCategory(int ProductId, int CategoryId)
        {
            ProductCategory pcToRemove = new ProductCategory();
            pcToRemove.CategoryId = CategoryId;
            pcToRemove.ProductId = ProductId;
            ShopContext.Remove(pcToRemove);
        }

        public void IncreaseCategoryViewCount(string categoryUrl)
        {
            Category category = ShopContext.Categories.Where(c => c.CategoryUrl == categoryUrl).FirstOrDefault();
            category.ViewCount += 1;
        }
    }
}
