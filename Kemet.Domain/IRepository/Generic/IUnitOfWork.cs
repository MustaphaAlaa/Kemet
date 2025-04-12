namespace IRepository.Generic;

public interface IUnitOfWork : IDisposable
{
    IBaseRepository<T> GetRepository<T>()
        where T : class;
    Task<int> CompleteAsync(); // Use async for best practice
}
