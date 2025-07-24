using Entities.Infrastructure;
using IRepository.Generic;

namespace Repositories.Generic;

public class RangeRepository<TEntity> : BaseRepository<TEntity>, IRangeRepository<TEntity>
    where TEntity : class
{
    // protected readonly KemetDbContext _db;
    private readonly KemetDbContext _db;

    public RangeRepository(KemetDbContext context) : base(context)
    {
        _db = context;
    }

    /// <summary>
    /// Create range of  entities in the database.
    /// </summary>
    /// <param name="entity">the entity type to be creat.</param> 
    public async Task AddRangeAsync(TEntity[] entity)
    {
        await _db.Set<TEntity>().AddRangeAsync(entity);
    }
}


