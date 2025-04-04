using System.Linq.Expressions;
using IRepository.Generic;
using Kemet.Infrastructure;
using Microsoft.EntityFrameworkCore;



namespace Repositories.Generic;
public class RetrieveAllRepository<Entity> : RepositoryDbContext, IRetrieveAllAsync<Entity> where Entity : class
{
    protected DbSet<Entity> _entities;

    public RetrieveAllRepository(KemetDbContext context) : base(context)
    {
        _entities = this._db.Set<Entity>();
    }

    public Task<List<Entity>> GetAllAsync()
    {
        return _entities.AsNoTracking().Select(entity => entity).ToListAsync();
    }

    public Task<IQueryable<Entity>> GetAllAsync(Expression<Func<Entity, bool>> predicate)
    {
        var queryable = _entities.AsNoTracking().Where(predicate);
        return Task.FromResult(queryable);
    }
}