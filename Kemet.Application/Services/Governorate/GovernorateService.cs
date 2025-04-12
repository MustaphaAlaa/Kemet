using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces.Helpers;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application;

public class GovernorateService : IGovernorateService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBaseRepository<Governorate> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GovernorateService> _logger;
    private readonly IGovernorateValidation _governorateValidation;
    private readonly IRepositoryRetrieverHelper<Governorate> _repositoryHelper;

    public GovernorateService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<GovernorateService> logger,
        IGovernorateValidation governorateValidation,
        IRepositoryRetrieverHelper<Governorate> repositoryHelper
    )
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Governorate>();

        _mapper = mapper;
        _logger = logger;
        _governorateValidation = governorateValidation;
        _repositoryHelper = repositoryHelper;
    }

    public async Task<GovernorateReadDTO> CreateAsync(GovernorateCreateDTO entity)
    {
        try
        {
            await _governorateValidation.ValidateCreate(entity);
            var governorate = _mapper.Map<Governorate>(entity);
            governorate = await _repository.CreateAsync(governorate);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GovernorateReadDTO>(governorate);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await _governorateValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.GovernorateId == entity.GovernorateId);
            var isDeleted = await _unitOfWork.SaveChangesAsync() > 0;
            return isDeleted;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the governorate.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<GovernorateReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<GovernorateReadDTO>();
    }

    public async Task<IEnumerable<GovernorateReadDTO>> RetrieveAllAsync(
        Expression<Func<Governorate, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<GovernorateReadDTO>(predicate);
    }

    public async Task<GovernorateReadDTO> RetrieveByAsync(
        Expression<Func<Governorate, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveByAsync<GovernorateReadDTO>(predicate);
    }

    public async Task<GovernorateReadDTO> UpdateAsync(GovernorateUpdateDTO updateRequest)
    {
        try
        {
            await _governorateValidation.ValidateUpdate(updateRequest);

            var governorate = _mapper.Map<Governorate>(updateRequest);

            var updatedGovernorate = _repository.Update(governorate);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<GovernorateReadDTO>(governorate);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
