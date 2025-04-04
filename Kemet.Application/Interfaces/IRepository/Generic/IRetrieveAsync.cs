using System.Linq.Expressions;

namespace IRepository.Generic;

public interface IRetrieveAsync<TResult>
{
    public Task<TResult?> GetAsync(Expression<Func<TResult, bool>> predicate);
}