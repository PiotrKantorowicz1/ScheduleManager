using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Manager.Core.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        void DeleteAsync(T entity);
        void DeleteWhereAsync(Expression<Func<T, bool>> predicate);
        Task Commit();
    }
}