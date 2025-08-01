using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class DeliveryCompanyService
    : GenericService<DeliveryCompany, DeliveryCompanyReadDTO, DeliveryCompanyService>,
        IDeliveryCompanyService
{
    private readonly IDeliveryCompanyRepository _repository;
    private readonly IDeliveryCompanyValidation _deliveryCompanyValidation;

    public DeliveryCompanyService(
        IServiceFacade_DependenceInjection<DeliveryCompany, DeliveryCompanyService> facadeDI,
        IDeliveryCompanyValidation governorateValidation,
        IDeliveryCompanyRepository repository
    )
        : base(facadeDI, "Delivery Company")
    {
        //_repository = _unitOfWork.GetRepository<DeliveryCompany>();
        _repository = repository;
        _deliveryCompanyValidation = governorateValidation;
    }

    public async Task<DeliveryCompany> CreateWithTrackingAsync(DeliveryCompanyCreateDTO entity)
    {
        try
        {
            await _deliveryCompanyValidation.ValidateCreate(entity);

            var deliveryCompany = _mapper.Map<DeliveryCompany>(entity);
            deliveryCompany.DialingWithItFrom = deliveryCompany.DialingWithItFrom.ToUniversalTime();
            deliveryCompany = await _repository.CreateAsync(deliveryCompany);

            return deliveryCompany;
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

    public async Task<DeliveryCompanyReadDTO> CreateAsync(DeliveryCompanyCreateDTO entity)
    {
        try
        {
            await _deliveryCompanyValidation.ValidateCreate(entity);

            var deliveryCompany = _mapper.Map<DeliveryCompany>(entity);
            deliveryCompany.DialingWithItFrom = deliveryCompany.DialingWithItFrom.ToUniversalTime();
            deliveryCompany = await _repository.CreateAsync(deliveryCompany);

            return _mapper.Map<DeliveryCompanyReadDTO>(deliveryCompany);
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

    public async Task<DeliveryCompanyReadDTO> Update(DeliveryCompanyUpdateDTO updateRequest)
    {
        try
        {
            await _deliveryCompanyValidation.ValidateUpdate(updateRequest);

            var governorateDelivery = _mapper.Map<DeliveryCompany>(updateRequest);

            var updatedDeliveryCompany = _repository.Update(governorateDelivery);

            return _mapper.Map<DeliveryCompanyReadDTO>(governorateDelivery);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"{TName} doesn't exist. {ex.Message}";
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

    public async Task DeleteAsync(DeliveryCompanyDeleteDTO entity)
    {
        try
        {
            await _deliveryCompanyValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(g => g.DeliveryCompanyId == entity.DeliveryCompanyId);
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

    public async Task<DeliveryCompanyReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(g => g.DeliveryCompanyId == key);
    }

    public async Task<bool> CheckDeliveryCompanyAvailability(int deliveryCompanyId)
    {
        try
        {
            var deliveryCompany = await this.RetrieveByAsync(dc =>
                dc.DeliveryCompanyId == deliveryCompanyId && dc.IsActive == true
            );

            return deliveryCompany != null;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while checking the {TName} availability. \n{ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<GovernorateDeliveryCompanyDTO>> ActiveGovernorates(
        int deliveryCompanyId
    )
    {
        var activeGDCQuery = _repository.ActiveGovernorates(deliveryCompanyId);
        var governoratesDCList = await activeGDCQuery.ToListAsync();

        var governorateDeliveryCompanyDtoLst = governoratesDCList.Select(
            d => new GovernorateDeliveryCompanyDTO
            {
                Name = d.Governorate.Name,
                GovernorateId = d.GovernorateId,
                DeliveryCost = d.DeliveryCost,
                IsActive = d.IsActive,
                DeliveryCompanyId = deliveryCompanyId,
                GovernorateDeliveryCompanyId = d.GovernorateDeliveryCompanyId,
            }
        );
        return governorateDeliveryCompanyDtoLst;
    }

    public async Task<ICollection<DeliveryCompanyReadDTO>> DeliveryCompanyForActiveGovernorate(int governorateId)
    {
        var ggg = await _repository.DeliveryCompanyForActiveGovernorate(governorateId).Select(gdc => new DeliveryCompanyReadDTO
        {
            DeliveryCompanyId = gdc.DeliveryCompanyId,
            DialingWithItFrom = gdc.DialingWithItFrom,
            IsActive = gdc.IsActive,
            Name = gdc.Name
        }).ToListAsync();

        return ggg;
    }
}
