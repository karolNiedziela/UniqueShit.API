using Microsoft.EntityFrameworkCore;
using UniqueShit.Application.Core.Queries;

namespace UniqueShit.Application.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PagedList<T>> PaginateAsync<T>(this IQueryable<T> data, 
            int pageNumber, 
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalItems = await data.CountAsync(cancellationToken);
            var totalPages = totalItems <= pageSize ? 1 : (int)Math.Ceiling((double)totalItems / pageSize);

            var result = await data.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedList<T>(result, pageNumber, pageSize, totalItems);
        }
    }
}
