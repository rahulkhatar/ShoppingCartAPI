using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart.Core.Interfaces
{
    public interface IGenericRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>>? predicate = null, string? includeProperties = null);
        Task<T?> GetById(Guid id);
        Task<T> Add(T entity);
        Task<(bool, T)> Update(Guid id, T entity);
        Task<bool> Delete(Guid id);
    }
}
