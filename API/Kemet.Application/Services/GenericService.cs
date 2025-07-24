using Entities.Models.DTOs;
using Entities.Models;
using IRepository.Generic;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Entities.Models.Interfaces.Helpers;
using AutoMapper;
using Kemet.Application.Interfaces;

namespace Application.Services;

public abstract class GenericService<T, TReadDTO, TService>
    where T : class
    where TReadDTO : class
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly ILogger<TService> _logger;
    protected readonly IRepositoryRetrieverHelper<T> _repositoryHelper;
    protected readonly IMapper _mapper;
    protected readonly string TName;
    //private readonly ServiceFacade_DependenceInjection<T> _facdeDI;
    protected GenericService(IServiceFacade_DependenceInjection<T, TService> facadeDI, string tName)
    {
        //_facdeDI = facadeDI;

        _logger = facadeDI.logger;
        _repositoryHelper = facadeDI.repositoryHelper;
        _mapper = facadeDI.mapper;
        _unitOfWork = facadeDI.unitOfWork;

        TName = tName;
    }

    public async Task<int> SaveAsync()
    {
        return await _unitOfWork.SaveChangesAsync();
    }

    public async Task<List<TReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<TReadDTO>();
        }
        catch (Exception ex)
        {
            string msg = $"Unexpected exception throws while retrieving {TName} records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<TReadDTO>> RetrieveAllAsync(Expression<Func<T, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<TReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg = $"Unexpected exception throws while retrieving {TName} records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<TReadDTO> RetrieveByAsync(Expression<Func<T, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<TReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg = $"Unexpected exception throws while retrieving the {TName} record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}



