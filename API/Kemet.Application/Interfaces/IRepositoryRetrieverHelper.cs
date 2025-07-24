using System.Linq.Expressions;
using AutoMapper;
using IRepository.Generic;

namespace Entities.Models.Interfaces.Helpers;

public interface IRepositoryRetrieverHelper<T>
    where T : class
{
    Task<List<DTO>> RetrieveAllAsync<DTO>();
    Task<IEnumerable<DTO>> RetrieveAllAsync<DTO>(Expression<Func<T, bool>> predicate);
    Task<DTO> RetrieveByAsync<DTO>(Expression<Func<T, bool>> predicate);
}
