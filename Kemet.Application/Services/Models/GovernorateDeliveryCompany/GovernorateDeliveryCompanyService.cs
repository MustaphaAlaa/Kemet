using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class GovernorateDeliveryCompanyService
    : GenericService<
        GovernorateDeliveryCompany,
        GovernorateDeliveryCompanyReadDTO,
        GovernorateDeliveryCompanyService
    >,
        IGovernorateDeliveryCompanyService
{
    private readonly IRangeRepository<GovernorateDeliveryCompany> _repository;
    private readonly IGovernorateDeliveryCompanyValidation _governorateDeliveryCompanyValidation;

    public GovernorateDeliveryCompanyService(
        IServiceFacade_DependenceInjection<
            GovernorateDeliveryCompany,
            GovernorateDeliveryCompanyService
        > facadeDI,
        IGovernorateDeliveryCompanyValidation governorateDeliveryCompanyValidation,
        IRangeRepository<GovernorateDeliveryCompany> repository
    )
        : base(facadeDI, "Governorate")
    {
        _repository = repository;
        _governorateDeliveryCompanyValidation = governorateDeliveryCompanyValidation;
    }

    public async Task<GovernorateDeliveryCompanyReadDTO> CreateAsync(
        GovernorateDeliveryCompanyCreateDTO createRequest
    )
    {
        try
        {
            await _governorateDeliveryCompanyValidation.ValidateCreate(createRequest);

            // to make sure if one is null the other one will be null
            if (createRequest.DeliveryCost is null || createRequest.IsActive is null)
            {
                createRequest.DeliveryCost = null;
                createRequest.IsActive = null;
            }

            var governorateDeliveryCompany = _mapper.Map<GovernorateDeliveryCompany>(createRequest);

            governorateDeliveryCompany = await _repository.CreateAsync(governorateDeliveryCompany);

            return _mapper.Map<GovernorateDeliveryCompanyReadDTO>(governorateDeliveryCompany);
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

    private async Task<GovernorateDeliveryCompany> CreateTrackedAsync(
        GovernorateDeliveryCompanyCreateDTO createRequest
    )
    {
        try
        {
            await _governorateDeliveryCompanyValidation.ValidateCreate(createRequest);

            // to make sure if one is null the other one will be null
            if (createRequest.DeliveryCost is null || createRequest.IsActive is null)
            {
                createRequest.DeliveryCost = null;
                createRequest.IsActive = null;
            }

            var governorateDeliveryCompany = _mapper.Map<GovernorateDeliveryCompany>(createRequest);

            governorateDeliveryCompany = await _repository.CreateAsync(governorateDeliveryCompany);

            return governorateDeliveryCompany;
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

    public async Task<GovernorateDeliveryCompanyReadDTO> Update(
        GovernorateDeliveryCompanyUpdateDTO updateRequest
    )
    {
        try
        {
            await _governorateDeliveryCompanyValidation.ValidateUpdate(updateRequest);

            var governorate = _mapper.Map<GovernorateDeliveryCompany>(updateRequest);

            var GovernorateDeliveryCompany = _repository.Update(governorate);

            return _mapper.Map<GovernorateDeliveryCompanyReadDTO>(governorate);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"GovernorateDeliveryCompany doesn't exist. {ex.Message}";
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

    public async Task DeleteAsync(GovernorateDeliveryCompanyDeleteDTO entity)
    {
        try
        {
            await _governorateDeliveryCompanyValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g =>
                g.GovernorateDeliveryCompanyId == entity.GovernorateDeliveryCompanyId
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

    public async Task<GovernorateDeliveryCompanyReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.GovernorateDeliveryCompanyId == key);
    }

    public async Task<bool> GovernorateDeliveryCompanyAvailability(
        int deliveryCompanyId,
        int governorateId
    )
    {
        try
        {
            var governorate =
                await _repositoryHelper.RetrieveByAsync<GovernorateDeliveryCompanyReadDTO>(dc =>
                    dc.DeliveryCompanyId == deliveryCompanyId
                    && dc.GovernorateId == governorateId
                    && dc.IsActive == true
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

    public async Task<GovernorateDeliveryCompanyReadDTO> Deactivate(
        int governorateDeliveryCompanyId
    )
    {
        try
        {
            var governoratedeliveryCompany = await _repository.RetrieveAsync(g =>
                g.GovernorateDeliveryCompanyId == governorateDeliveryCompanyId
            );

            Utility.DoesExist(governoratedeliveryCompany, "Governorate Delivery Company");

            governoratedeliveryCompany.IsActive = false;

            var updatedGovernorateDeliveryCompany = _repository.Update(governoratedeliveryCompany);

            return _mapper.Map<GovernorateDeliveryCompanyReadDTO>(
                updatedGovernorateDeliveryCompany
            );
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"GovernorateDeliveryCompany doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the deactivate of the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<IEnumerable<GovernorateDeliveryCompanyReadDTO>> GetAllActiveGovernorate(
        int governorateDeliveryCompanyId
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<GovernorateDeliveryCompanyReadDTO>(g =>
            g.GovernorateDeliveryCompanyId == governorateDeliveryCompanyId
            && (g.IsActive == true || g.IsActive == null)
        );
    }

    public async Task AddRange(IEnumerable<GovernorateDeliveryCompanyCreateDTO> entities)
    {
        var e = _mapper.Map<List<GovernorateDeliveryCompany>>(entities);
        await _repository.AddRangeAsync(e.ToArray());
    }

    public async Task<GovernorateDeliveryCompanyReadDTO> SoftUpdate(
        GovernorateDeliveryCompanyUpdateDTO updateRequest
    )
    {
        try
        {
            _logger.LogInformation($"{this} => SoftUpdate({updateRequest})");

            var governorateDeliveryCompany = await _repository.RetrieveTrackedAsync(gdc =>
                gdc.GovernorateDeliveryCompanyId == updateRequest.GovernorateDeliveryCompanyId
                && (gdc.IsActive == null || gdc.IsActive == true)
            );

            Utility.DoesExist(governorateDeliveryCompany, "Governorate Delivery Company");

            GovernorateDeliveryCompanyReadDTO governorateDeliveryCompanyReadDTO = new();

            if (governorateDeliveryCompany.IsActive is null)
            {
                governorateDeliveryCompany.IsActive = true;
                governorateDeliveryCompany.DeliveryCost = updateRequest.DeliveryCost;

                var newGovernorateDeliveryCompanyDeliveryCompany = _repository.Update(
                    governorateDeliveryCompany
                );
                await this.SaveAsync();

                governorateDeliveryCompanyReadDTO = _mapper.Map<GovernorateDeliveryCompanyReadDTO>(
                    newGovernorateDeliveryCompanyDeliveryCompany
                );
            }
            else
            {
                governorateDeliveryCompany.IsActive = false;
                _repository.Update(governorateDeliveryCompany);
                var dto = new GovernorateDeliveryCompanyCreateDTO
                {
                    GovernorateId = governorateDeliveryCompany.GovernorateId,
                    DeliveryCost = updateRequest.DeliveryCost,
                    DeliveryCompanyId = governorateDeliveryCompany.DeliveryCompanyId,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                };
                var newGDC = await this.CreateTrackedAsync(dto);
                await this.SaveAsync();
                governorateDeliveryCompanyReadDTO = _mapper.Map<GovernorateDeliveryCompanyReadDTO>(
                    newGDC
                );
            }
            return governorateDeliveryCompanyReadDTO;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                $"{this}, exception thrown while occurred SoftUpdate operation, {updateRequest}: {ex.Message}",
                ex
            );
            throw;
        }
    }
}
