using Entities.Models;

namespace IRepository.Generic;

public interface IRangeRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : class
{
    Task  AddRangeAsync(TEntity[] entity);
    
}