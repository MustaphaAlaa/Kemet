using System.Linq.Expressions;

namespace Domain.IServices;

/// <summary>
/// Defines a generic, asynchronous interface for services that handle the creation of new records in a database.
/// </summary>
/// <typeparam name="T">The type of the entity that represents the record to be created.</typeparam>
/// <typeparam name="TKey">The type of the id key.</typeparam>
/// <typeparam name="TCreate">The DTO for creation operation.</typeparam>
/// <typeparam name="TDelete">The DTO for delete operation.</typeparam>
/// <typeparam name="TUpdate">The DTO for update operation.</typeparam>
/// <typeparam name="TResult">The type of the result object returned by the creation operation.</typeparam>
public interface IServiceAsync<T, TKey, TCreate, TDelete, TUpdate, TResult>
{
    /// <summary>
    /// Asynchronously contains the business logic before marks an objected to be a new record into the database, without actually create it.
    /// </summary>
    /// <param name="entity">The object containing the data for the new record to insert.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// This task contains the result of the creation operation, or an exception if insertion failed.
    /// The returned result typically contains the newly created object, with any additional properties
    /// assigned after insertion such as ids.
    /// </returns>
    public Task<TResult> CreateAsync(TCreate entity);

    Task DeleteAsync(TDelete entity);

    /// <summary>
    /// Asynchronously retrieves all records;
    /// </summary>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The result contains all records from the database.
    /// </returns>
    public Task<List<TResult>> RetrieveAllAsync();

    /// <summary>
    /// Asynchronously retrieves all records based on the specified predicate condition.
    /// </summary>
    /// <param name="predicate">An expression that defines the condition for retrieving the records.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The result contains the records that matches the specified predicate.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    public Task<IEnumerable<TResult>> RetrieveAllAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Asynchronously retrieves a record based on the specified predicate condition.
    /// </summary>
    /// <param name="predicate">An expression that defines the condition for retrieving the record.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// The result contains the record that matches the specified predicate.
    /// </returns>
    /// <exception cref="ArgumentNullException">Thrown if the predicate is null.</exception>
    Task<TResult> RetrieveByAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Marks a record to updates without update it in database.
    /// </summary>
    /// <param name="updateRequest">The object containing the data for the record to update.</param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation.
    /// This task contains the result of the update operation, or an exception if the update failed.
    /// The returned result typically contains the object that will be update, with any additional properties
    /// assigned after the update.
    /// </returns>
    Task<TResult> Update(TUpdate updateRequest);

    /// <summary>
    /// Asynchronously retrieves a record by its unique identifier.
    /// </summary>
    /// <param name="key">the unique identifier for <see cref="key"/>aaaaa</param>
    /// <returns> <see cref="Task{TResult}"/> </returns>
    Task<TResult> GetById(TKey key);


    Task<int> SaveAsync();
}
