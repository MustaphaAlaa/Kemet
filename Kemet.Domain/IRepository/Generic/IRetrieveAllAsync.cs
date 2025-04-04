using System.Linq.Expressions;

namespace IRepository.Generic;

public interface IRetrieveAllAsync<T>
{
    public Task<List<T>> GetAllAsync();
    public Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
}