using Kemet.Infrastructure;

namespace Repositories.Generic;

public abstract class RepositoryDbContext
{
    protected readonly KemetDbContext _db;


    public RepositoryDbContext(KemetDbContext context)
    {
        _db = context;
    }

    protected async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }
}