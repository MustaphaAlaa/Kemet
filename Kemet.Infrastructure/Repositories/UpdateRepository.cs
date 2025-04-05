
using IRepository.Generic;
using Kemet.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repositories.Generic;

public class UpdateRepository<Entity> : RepositoryDbContext, IUpdateAsync<Entity> where Entity : class
{
    protected DbSet<Entity> _entity;

    public UpdateRepository(KemetDbContext context) : base(context)
    {
        _entity = this._db.Set<Entity>();
    }

    public async Task<Entity> UpdateAsync(Entity entity)
    {
        this._entity.Update(entity);
        await this.SaveChangesAsync();
        return entity;
    }
}