using Data.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Concrete.EfCore
{
    public class EfCoreGenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly DbContext context;

        public EfCoreGenericRepository(DbContext context)
        {
            this.context = context;
        }

        public void Create(TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            Console.WriteLine("in generic repository");
            return await context.Set<TEntity>().ToListAsync();
        }

        public TEntity GetById(int id)
        {
            return context.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
