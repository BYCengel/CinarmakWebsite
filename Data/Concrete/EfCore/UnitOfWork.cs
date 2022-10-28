using Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShopContext _shopContext;

        public UnitOfWork(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        private EfCoreProductRepository productRepository;
        private EfCoreCategoryRepository categoryRepository;
        private EfCoreImageRepository imageRepository;

        public IProductRepository ProductRepository => productRepository = productRepository ?? new EfCoreProductRepository(_shopContext);

        public ICategoryRepository CategoryRepository => categoryRepository = categoryRepository ?? new EfCoreCategoryRepository(_shopContext);

        public IImageRepository ImageRepository => imageRepository = imageRepository ?? new EfCoreImageRepository(_shopContext);

        public void Dispose()
        {
            _shopContext.Dispose();
        }

        public void Save()
        {
            _shopContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _shopContext.SaveChangesAsync();
        }
    }
}
