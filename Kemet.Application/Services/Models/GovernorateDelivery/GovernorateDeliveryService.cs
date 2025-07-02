using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class GovernorateDeliveryService
    : GenericService<GovernorateDelivery, GovernorateDeliveryReadDTO, GovernorateDeliveryService>,
        IGovernorateDeliveryService
{
    private readonly IBaseRepository<GovernorateDelivery> _repository;
    private readonly IGovernorateDeliveryValidation _governorateDeliveryValidation;

    public GovernorateDeliveryService(
        IServiceFacade_DependenceInjection<
            GovernorateDelivery,
            GovernorateDeliveryService
        > facadeDI,
        IGovernorateDeliveryValidation governorateValidation
    )
        : base(facadeDI, "GovernorateDelivery")
    {
        _repository = _unitOfWork.GetRepository<GovernorateDelivery>();
        _governorateDeliveryValidation = governorateValidation;
    }

    public async Task<GovernorateDeliveryReadDTO> CreateAsync(GovernorateDeliveryCreateDTO entity)
    {
        try
        {
            await _governorateDeliveryValidation.ValidateCreate(entity);

            var governorateDelivery = _mapper.Map<GovernorateDelivery>(entity);

            governorateDelivery.CreatedAt = DateTime.UtcNow;

            governorateDelivery = await _repository.CreateAsync(governorateDelivery);

            return _mapper.Map<GovernorateDeliveryReadDTO>(governorateDelivery);
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

    public async Task<GovernorateDeliveryReadDTO> Update(GovernorateDeliveryUpdateDTO updateRequest)
    {
        try
        {
            await _governorateDeliveryValidation.ValidateUpdate(updateRequest);
            var governorateDelivery = await _repository.RetrieveTrackedAsync(g =>
                g.GovernorateDeliveryId == updateRequest.GovernorateDeliveryId
            );

            Utility.DoesExist(governorateDelivery, "GovernorateDelivery");

            if (governorateDelivery?.IsActive is null)
            {
                governorateDelivery.IsActive = true;
                governorateDelivery.DeliveryCost = updateRequest.DeliveryCost;
                governorateDelivery.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                governorateDelivery.IsActive = false;
            }

            var updatedGovernorateDelivery = _repository.Update(governorateDelivery);

            return _mapper.Map<GovernorateDeliveryReadDTO>(governorateDelivery);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"GovernorateDelivery doesn't exist. {ex.Message}";
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

    public async Task DeleteAsync(GovernorateDeliveryDeleteDTO entity)
    {
        try
        {
            await _governorateDeliveryValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g =>
                g.GovernorateDeliveryId == entity.GovernorateDeliveryId
            );
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

    public async Task<GovernorateDeliveryReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(g => g.GovernorateDeliveryId == key);
    }

    public async Task<bool> CheckGovernorateDeliveryAvailability(int governorateId)
    {
        try
        {
            var governorateDelivery = await this.RetrieveByAsync(g =>
                g.GovernorateId == governorateId && g.IsActive == true || g.IsActive == null
            );

            return governorateDelivery != null;
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
