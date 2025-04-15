using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
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

    public async Task<GovernorateReadDTO> CreateInternalAsync(GovernorateCreateDTO entity)
    {
        try
        {
            var governorateDto = await this.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return governorateDto;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<GovernorateReadDTO> CreateAsync(GovernorateCreateDTO entity)
    {
        try
        {
            await _governorateValidation.ValidateCreate(entity);

            var governorate = _mapper.Map<Governorate>(entity);

            governorate = await _repository.CreateAsync(governorate);

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

    public async Task DeleteAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await _governorateValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.GovernorateId == entity.GovernorateId);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the governorate.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<bool> DeleteInternalAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await this.DeleteAsync(entity);
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

    public async Task<GovernorateReadDTO> UpdateInternalAsync(GovernorateUpdateDTO updateRequest)
    {
        try
        {
            var governorate = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return governorate;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the governorate. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

    public async Task<GovernorateReadDTO> Update(GovernorateUpdateDTO updateRequest)
    {
        try
        {
            await _governorateValidation.ValidateUpdate(updateRequest);

            var governorate = _mapper.Map<Governorate>(updateRequest);

            var updatedGovernorate = _repository.Update(governorate);

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
