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
        private readonly ManagerDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public RepositoryBase(ManagerDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate)
             => await _context.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            return await query.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();

        public async Task<PagedResult<T>> GetAllPageable()
            => await _context.Set<T>().PaginateAsync();

        public async Task<PagedResult<T>> GetAllPageable<TQuery>(Expression<Func<T, bool>> predicate, TQuery query)
            where TQuery : PagedQueryBase
            => await _context.Set<T>().Where(predicate).PaginateAsync(); 

        public virtual async Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().Where(predicate).ToArrayAsync();

        public virtual async Task AddAsync(T entity)
        {
            await _unitOfWork.Set<T>().AddAsync(entity);
            await _unitOfWork.SaveChangesAsync(); 
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _unitOfWork.Set<T>().Update(entity);
            await _unitOfWork.SaveChangesAsync(); 
        }
        public virtual async Task DeleteAsync(T entity)
        {
            _unitOfWork.Set<T>().Remove(entity); 
            await _unitOfWork.SaveChangesAsync(); 
        }

        public virtual async Task DeleteWhereAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = _unitOfWork.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                _unitOfWork.Set<T>().Remove(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}