using System.Linq.Expressions;
using IRepository.Generic;
using Kemet.Infrastructure;
using Microsoft.EntityFrameworkCore;



namespace Repositorties.Generic;
public class RetrieveRepository<TEntity> : RepositoryDbContext, IRetrieveAsync<TEntity> where TEntity : class
{
    protected DbSet<TEntity> _entities;

    public RetrieveRepository(KemetDbContext context) : base(context)
    {
        _entities = this._db.Set<TEntity>();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _entities.AsNoTracking().FirstOrDefaultAsync(predicate); 
    }

}