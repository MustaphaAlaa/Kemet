using System.Linq.Expressions;

namespace IRepository.Generic;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity);
    Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
    Task<List<TEntity>> RetrieveAllAsync();
    Task<IEnumerable<TEntity>> RetrieveAllAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> RetrieveAsync(Expression<Func<TEntity, bool>> predicate);
    TEntity Update(TEntity entity);
}