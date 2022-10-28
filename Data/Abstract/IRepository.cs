using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Abstract
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int id);
        T GetById(int id);
        Task<List<T>> GetAllAsync();
        void Create(T entity);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
