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
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class GovernorateService
    : GenericService<Governorate, GovernorateReadDTO, GovernorateService>,
        IGovernorateService
{
    private readonly IBaseRepository<Governorate> _repository;
    private readonly IGovernorateValidation _governorateValidation;
 
    public GovernorateService(
        IServiceFacade_DependenceInjection<Governorate, GovernorateService> facadeDI,
        IGovernorateValidation governorateValidation
    )
        : base(facadeDI, "Governorate")
    {
        _repository = _unitOfWork.GetRepository<Governorate>();
        _governorateValidation = governorateValidation;
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
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"{TName} is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
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
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
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
                $"An error thrown while validating the updating of the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
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
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<GovernorateReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.GovernorateId == key);
    }

    public async Task<bool> CheckGovernorateAvailability(int governorateId)
    {
        try
        {
            var governorate = await this.RetrieveByAsync(g =>
                g.GovernorateId == governorateId && g.IsAvailableToDeliver == true
            );

            return governorate != null;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while checking the {TName} availability. \n{ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
