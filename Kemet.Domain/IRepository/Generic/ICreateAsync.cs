namespace IRepository.Generic;

public interface ICreateAsync<T>
{
    public Task<T> CreateAsync(T entity);
}