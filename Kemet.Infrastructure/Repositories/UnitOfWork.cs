using IRepository.Generic;
using Kemet.Infrastructure;

namespace Repositories.Generic;

public class UnitOfWork : IUnitOfWork
{
    private readonly KemetDbContext _db;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(KemetDbContext context)
    {
        _db = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public IBaseRepository<T> GetRepository<T>()
        where T : class
    {
        var type = typeof(T);
        if (!_repositories.ContainsKey(type))
        {
            var repo = new BaseRepository<T>(_db);
            _repositories.Add(type, repo);
        }

        return (IBaseRepository<T>)_repositories[type];
    }
}
