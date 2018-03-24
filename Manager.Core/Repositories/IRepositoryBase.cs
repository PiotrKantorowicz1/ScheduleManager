using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Manager.Core.Types;

namespace Manager.Core.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedResult<T>> GetAllPageable();
        Task<PagedResult<T>> GetAllPageable<TQuery>(Expression<Func<T, bool>> predicate, TQuery query)
            where TQuery : PagedQueryBase;
        Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task DeleteWhereAsync(Expression<Func<T, bool>> predicate);
    }
}