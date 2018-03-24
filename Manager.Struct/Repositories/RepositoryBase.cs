using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Manager.Struct.EF;
using System.Threading.Tasks;
using Manager.Core.Repositories;
using Manager.Core.Types;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Manager.Struct.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryBase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
             => await _unitOfWork.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _unitOfWork.Set<T>();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => await _unitOfWork.Set<T>().ToListAsync();

        public async Task<PagedResult<T>> GetAllPageable()
            => await _unitOfWork.Set<T>().PaginateAsync();

        public async Task<PagedResult<T>> GetAllPageable<TQuery>(Expression<Func<T, bool>> predicate, TQuery query)
            where TQuery : PagedQueryBase
            => await _unitOfWork.Set<T>().Where(predicate).PaginateAsync(); 

        public virtual async Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _unitOfWork.Set<T>();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
            => await _unitOfWork.Set<T>().Where(predicate).ToArrayAsync();

        public virtual async Task AddAsync(T entity)
        {
            await _unitOfWork.Set<T>().AddAsync(entity);
        }

        public virtual void Update(T entity)
        {
            _unitOfWork.Set<T>().Update(entity); 
        }
        public virtual void Delete(T entity)
        {
            _unitOfWork.Set<T>().Remove(entity); 
        }

        public virtual void DeleteWhere(Expression<Func<T, bool>> predicate)
        {
            var entities = _unitOfWork.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _unitOfWork.Set<T>().Remove(entity);
            }
        }

        public async Task Commit()
        {
            await _unitOfWork.SaveChangesAsync();
        }
    }
}