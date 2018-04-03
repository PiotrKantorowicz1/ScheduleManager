using System;
using System.Linq;
using System.Threading.Tasks;
using Manager.Core.Types;
using Microsoft.EntityFrameworkCore;

namespace Manager.Core.Repositories
{
    public static class Pagination
    {
        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> queryable, PagedQueryBase query)
            => await queryable.PaginateAsync(query.Page, query.Results);

        public static async Task<PagedResult<T>> PaginateAsync<T>(this IQueryable<T> queryable,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            var isEmpty = await queryable.AnyAsync() == false;
            if (isEmpty)
            {
                return PagedResult<T>.Empty;
            }
            var totalResults = await queryable.CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalResults / resultsPerPage);
            var data = await queryable.Limit(page, resultsPerPage).ToListAsync();

            return PagedResult<T>.Create(data, page, resultsPerPage, totalPages, totalResults);
        }

        public static IQueryable<T> Limit<T>(this IQueryable<T> queryable, PagedQueryBase query)
            => queryable.Limit(query.Page, query.Results);

        public static IQueryable<T> Limit<T>(this IQueryable<T> queryable,
            int page = 1, int resultsPerPage = 10)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (resultsPerPage <= 0)
            {
                resultsPerPage = 10;
            }
            var skip = (page - 1) * resultsPerPage;
            var data = queryable.Skip(skip)
                .Take(resultsPerPage);

            return data;
        }
    }
}