using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services;

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

    #region Create
    private async Task<GovernorateReadDTO> CreateGovernorateCore(GovernorateCreateDTO entity)
    {
        await _governorateValidation.ValidateCreate(entity);

        var governorate = _mapper.Map<Governorate>(entity);

        governorate = await _repository.CreateAsync(governorate);

        return _mapper.Map<GovernorateReadDTO>(governorate);
    }

    public async Task<GovernorateReadDTO> CreateInternalAsync(GovernorateCreateDTO entity)
    {
        try
        {
            var governorateDto = await this.CreateGovernorateCore(entity);
            await _unitOfWork.SaveChangesAsync();
            return governorateDto;
        }
        catch (ValidationException ex)
        {
            string msg =
                $"Validating Exception is thrown while creating the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"Governorate is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<GovernorateReadDTO> CreateAsync(GovernorateCreateDTO entity)
    {
        try
        {
            var governorate = await CreateGovernorateCore(entity);
            return governorate;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"Governorate is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }
    #endregion



    #region  Update
    private async Task<GovernorateReadDTO> UpdateCore(GovernorateUpdateDTO updateRequest)
    {
        await _governorateValidation.ValidateUpdate(updateRequest);

        var governorate = _mapper.Map<Governorate>(updateRequest);

        var updatedGovernorate = _repository.Update(governorate);

        return _mapper.Map<GovernorateReadDTO>(governorate);
    }

    public async Task<GovernorateReadDTO> UpdateInternalAsync(GovernorateUpdateDTO updateRequest)
    {
        try
        {
            var governorate = await this.UpdateCore(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return governorate;
        }
        catch (ValidationException ex)
        {
            string msg =
                $"Validating Exception is thrown while updating the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Governorate doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<GovernorateReadDTO> Update(GovernorateUpdateDTO updateRequest)
    {
        try
        {
            var governorate = await this.UpdateCore(updateRequest);
            return governorate;
        }
        catch (ValidationException ex)
        {
            string msg =
                $"Validating Exception is thrown while updating the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Governorate doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }
    #endregion



    #region  Delete
    private async Task DeleteCore(GovernorateDeleteDTO entity)
    {
        await _governorateValidation.ValidateDelete(entity);
        await _repository.DeleteAsync(g => g.GovernorateId == entity.GovernorateId);
    }

    public async Task DeleteAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await DeleteCore(entity);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<bool> DeleteInternalAsync(GovernorateDeleteDTO entity)
    {
        try
        {
            await this.DeleteCore(entity);
            var isDeleted = await _unitOfWork.SaveChangesAsync() > 0;
            return isDeleted;
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the governorate. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }
    #endregion



    #region  Retrieve
    public async Task<List<GovernorateReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<GovernorateReadDTO>();
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving governorate records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<GovernorateReadDTO>> RetrieveAllAsync(
        Expression<Func<Governorate, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<GovernorateReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving governorate records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<GovernorateReadDTO> RetrieveByAsync(
        Expression<Func<Governorate, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<GovernorateReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving the governorate record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
    #endregion
}
