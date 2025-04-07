namespace Domain.IServices;

public interface IDeleteServiceAsync<TKey>
{
    Task<bool> DeleteAsync(TKey id);
}