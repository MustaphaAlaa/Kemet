using Microsoft.EntityFrameworkCore;

namespace Entities.Infrastructure.Extensions;

public static class IQueryableExtension
{
    public static async Task<PaginatedResult<T>> ToPaginateListAsync<T>(
        this IQueryable<T> source,
        int page,
        int pageSize
    )
    {
        var count = await source.CountAsync();
        var totalPages = (int)Math.Ceiling(count / (double)pageSize);

        var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedResult<T>
        {
            CurrentPage = page,
            PageSize = pageSize,
            TotalPages = totalPages,
            TotalCount = count,
            Data = items,
            HasNext = page < totalPages,
            HasPrevious = page > 1,
        };
    }
}
