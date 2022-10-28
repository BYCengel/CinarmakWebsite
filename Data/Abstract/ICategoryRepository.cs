using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> GetByIdWithProductsAsync(int categoryId);
        void RemoveFromCategory(int ProductId, int CategoryId);
        void IncreaseCategoryViewCount(string categoryUrl);
    }
}
