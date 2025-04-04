namespace IRepository.Generic;

public interface IUpdateAsync<T>
{
    public Task<T> UpdateAsync(T entity);
}