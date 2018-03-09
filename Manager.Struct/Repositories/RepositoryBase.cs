using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Manager.Struct.EF;
using System.Threading.Tasks;
using Manager.Core.Repositories;

namespace Manager.Struct.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ManagerDbContext _context;

        public RepositoryBase(ManagerDbContext context)
        {
            _context = context;
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

        public virtual async Task<IEnumerable<T>> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query = includeProperties.Aggregate(query,
                (current, includeProperty) => current.Include(includeProperty));

            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
            => await _context.Set<T>().Where(predicate).ToArrayAsync();

        public virtual async Task<int> CountAsync()
            => await _context.Set<T>().CountAsync();

        public virtual async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }
        public virtual void DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual void DeleteWhereAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = _context.Set<T>().Where(predicate);

            foreach (var entity in entities)
            {
                DeleteAsync(entity);
            }
        }

        public virtual async Task Commit()
            => await _context.SaveChangesAsync();
    }
}