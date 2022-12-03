using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected DbContext context;
        protected DbSet<T> table;
        public GenericRepository(DbContext context)
        {
            this.context = context;
            table = context.Set<T>();
        }
        public async Task AddAsync(T entity) => await table.AddAsync(entity);

        public Task DeleteAsync(T entity)
        {
            return Task.Factory.StartNew(() => table.Remove(entity));
        }

        public virtual async Task<T> GetAsync(int id) => await table.FindAsync(id);

        public IEnumerable<T> GetAll() => table;

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        public Task UpdateAsync(T entity) => Task.Factory.StartNew(() => table.Update(entity));
    }
}
