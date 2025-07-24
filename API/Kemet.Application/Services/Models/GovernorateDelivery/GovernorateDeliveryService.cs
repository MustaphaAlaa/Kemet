using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class GovernorateDeliveryService
    : GenericService<GovernorateDelivery, GovernorateDeliveryReadDTO, GovernorateDeliveryService>,
        IGovernorateDeliveryService
{
    private readonly IGovernorateDeliveryRepository _repository;
    private readonly IGovernorateDeliveryValidation _governorateDeliveryValidation;

    public GovernorateDeliveryService(
        IServiceFacade_DependenceInjection<
            GovernorateDelivery,
            GovernorateDeliveryService
        > facadeDI,
        IGovernorateDeliveryValidation governorateValidation,
        IGovernorateDeliveryRepository repository
    )
        : base(facadeDI, "GovernorateDelivery")
    {
        _repository = repository;
        _governorateDeliveryValidation = governorateValidation;
    }

    private async Task<GovernorateDelivery> CreateCore(GovernorateDeliveryCreateDTO entity)
    {
        await _governorateDeliveryValidation.ValidateCreate(entity);

        var governorateDelivery = _mapper.Map<GovernorateDelivery>(entity);

        governorateDelivery.CreatedAt = DateTime.UtcNow;

        return await _repository.CreateAsync(governorateDelivery);
    }

    public async Task<GovernorateDeliveryReadDTO> CreateAsync(GovernorateDeliveryCreateDTO entity)
    {
        try
        {
            var governorateDelivery = await CreateCore(entity);
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

    private async Task<GovernorateDelivery> DoesExist(int id)
    {
        var gd = await _repository.RetrieveTrackedAsync(g => g.GovernorateDeliveryId == id);

        Utility.DoesExist(gd, "GovernorateDelivery");

        return gd;
    }

    public async Task<GovernorateDeliveryReadDTO> DeactivateAndCreate(
        GovernorateDeliveryUpdateDTO updateRequest
    )
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var governorateDelivery = await this.DoesExist(updateRequest.GovernorateDeliveryId);

            if (
                governorateDelivery.DeliveryCost == updateRequest.DeliveryCost
                && updateRequest.IsActive == governorateDelivery.IsActive
            )
                throw new Exception("Can not add new Record with same data");

            bool deactivated = false;
            if (governorateDelivery?.IsActive is null)
            {
                governorateDelivery.IsActive = true;
                governorateDelivery.DeliveryCost = updateRequest.DeliveryCost;
                governorateDelivery.CreatedAt = DateTime.UtcNow;
            }
            else
            {
                governorateDelivery.IsActive = false;
                deactivated = true;
            }

            GovernorateDelivery newGovernorateDelivery = null;

            newGovernorateDelivery = _repository.Update(governorateDelivery);
            await this.SaveAsync();

            if (deactivated)
            {
                var newGD = new GovernorateDelivery
                {
                    CreatedAt = DateTime.UtcNow,
                    DeliveryCost = updateRequest.DeliveryCost,
                    GovernorateId = governorateDelivery.GovernorateId,
                    IsActive = true,
                };

                await this.SaveAsync();

                await _unitOfWork.CommitAsync();
            }

            await _unitOfWork.CommitAsync();

            return _mapper.Map<GovernorateDeliveryReadDTO>(newGovernorateDelivery);
        }
        catch (DoesNotExistException ex)
        {
            await _unitOfWork.RollbackAsync();
            string msg = $"GovernorateDelivery doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            string msg = $"An error thrown while trying handle {TName} soft update. {ex.Message}";
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

    public async Task<ICollection<GovernorateDeliveryDTO>> ActiveGovernoratesDelivery()
    {
        var lst = _repository
            .ActiveGovernoratesDelivery()
            .Select(gd => new GovernorateDeliveryDTO
            {
                GovernorateDeliveryId = gd.GovernorateDeliveryId,
                GovernorateId = gd.GovernorateId,
                IsActive = gd.IsActive,
                DeliveryCost = gd.DeliveryCost,
                Name = gd.Governorate.Name,
            });

        return await lst.ToListAsync();
    }

    public async Task<ICollection<GovernorateDeliveryDTO>> NullableActiveGovernoratesDelivery()
    {
        var lst = _repository
            .NullableActiveGovernoratesDelivery()
            .Select(gd => new GovernorateDeliveryDTO
            {
                GovernorateDeliveryId = gd.GovernorateDeliveryId,
                GovernorateId = gd.GovernorateId,
                IsActive = gd.IsActive,
                DeliveryCost = gd.DeliveryCost,
                Name = gd.Governorate.Name,
            });

        return await lst.ToListAsync();
    }
}
