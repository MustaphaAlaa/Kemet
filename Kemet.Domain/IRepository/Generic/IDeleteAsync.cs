using System.Linq.Expressions;

namespace IRepository.Generic;

public interface IDeleteAsync<T>
{
    public Task<int> DeleteAsync(Expression<Func<T, bool>> predicate);
}