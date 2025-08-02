using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ShoppingCart.Core.Interfaces;
using ShoppingCart.Infrastucture.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Infrastucture.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ProductDbContext _context;
        private DbSet<T> _dbSet;
        public GenericRepository(ProductDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> Add(T entity)
        {
            var newEntry = await _dbSet.AddAsync(entity);
            return newEntry.Entity;
        }

        public async Task<bool> Delete(Guid id)
        {
            var findRecord = await _dbSet.FindAsync(id);
            if(findRecord != null)
            {
                var deletedRecord = _dbSet.Remove(findRecord);
                if (deletedRecord != null)
                    return true;
            }

            return false;
        }        

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null)
        {
            IQueryable<T> query = _dbSet;
            
            if(predicate != null)
                query = query.Where(predicate);

            if(includeProperties != null)
            {
                foreach (var item in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(item);
                }
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }
        
        public async Task<(bool, T)> Update(Guid id, T entity)
        {
            if (id == Guid.Empty)
            {
                return (false, entity); // Invalid ID provided
            }

            var entityIdProperty = typeof(T).GetProperty("Id");
            if (entityIdProperty != null && !id.Equals(entityIdProperty.GetValue(entity)))
            {
                entityIdProperty.SetValue(entity, id);
            }

            var success = false;
            while (!success)
            {
                try
                {
                    _dbSet.Update(entity);
                    return (true, entity);
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var databaseValues = await entry.GetDatabaseValuesAsync();

                    if (databaseValues == null)
                    {
                        return (false, entity);
                    }

                    entry.OriginalValues.SetValues(databaseValues);
                }
            }

            return (false, entity);
        }
    }
}
