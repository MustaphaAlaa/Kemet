using System.Linq.Expressions;
using AutoMapper;
using IRepository.Generic;
using Entities.Models.Interfaces.Helpers;

namespace Entities.Models.Utilities;

public class RepositoryRetrieverHelper<T> : IRepositoryRetrieverHelper<T>
    where T : class
{
    private readonly IBaseRepository<T> _repository;
    private IMapper _mapper;

    public RepositoryRetrieverHelper(IBaseRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<DTO>> RetrieveAllAsync<DTO>()
    {
        var lst = await _repository.RetrieveAllAsync();

        return lst.Select(p => _mapper.Map<DTO>(p)).ToList();
    }

    public async Task<IEnumerable<DTO>> RetrieveAllAsync<DTO>(Expression<Func<T, bool>> predicate)
    {
        var lst = await _repository.RetrieveAllAsync(predicate);

        return lst.Select(p => _mapper.Map<DTO>(p)).ToList();
    }

    public async Task<DTO> RetrieveByAsync<DTO>(Expression<Func<T, bool>> predicate)
    {
        var product = await _repository.RetrieveAsync(predicate);

        return _mapper.Map<DTO>(product);
    }
}
