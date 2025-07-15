using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class DeliveryCompanyValidation : IDeliveryCompanyValidation
{
    private readonly IBaseRepository<DeliveryCompany> _repository;

    // private readonly IBaseRepository<Order> _orderRepository;

    private readonly ILogger<DeliveryCompanyValidation> _logger;
    private readonly IValidator<DeliveryCompanyCreateDTO> _createValidator;
    private readonly IValidator<DeliveryCompanyUpdateDTO> _updateValidator;
    private readonly IValidator<DeliveryCompanyDeleteDTO> _deleteValidator;

    public DeliveryCompanyValidation(
        IBaseRepository<DeliveryCompany> repository,
        ILogger<DeliveryCompanyValidation> logger,
        IValidator<DeliveryCompanyCreateDTO> createValidator,
        IValidator<DeliveryCompanyUpdateDTO> updateValidator,
        IValidator<DeliveryCompanyDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(DeliveryCompanyCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            entity = this.Normalize(entity);

            var deliveryCompany = await _repository.RetrieveAsync(g => g.Name == entity.Name);

            Utility.AlreadyExist(deliveryCompany, "DeliveryCompany");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the deliveryCompany. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(DeliveryCompanyDeleteDTO entity)
    {
        try
        {
            var deliveryCompany = await _repository.RetrieveWithIncludeAsync(
                dc => dc.DeliveryCompanyId == entity.DeliveryCompanyId,
                dc => dc.Orders
            );

            if (deliveryCompany?.Orders.Count > 0)
                throw new Exception(
                    "There're orders associated with this delivery company so it cannot be deleted"
                );

            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the delivery company. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(DeliveryCompanyUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var deliveryCompany = await _repository.RetrieveAsync(g =>
                g.DeliveryCompanyId == entity.DeliveryCompanyId
            );

            Utility.DoesExist(deliveryCompany, "DeliveryCompany");

            entity = this.Normalize(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the delivery Company. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    private T Normalize<T>(T entity)
    {
        if (entity is DeliveryCompanyCreateDTO create)
        {
            create.Name = create?.Name?.Trim().ToLower();
        }

        if (entity is DeliveryCompanyUpdateDTO update)
        {
            update.Name = update?.Name?.Trim().ToLower();
        }

        return entity;
    }
}
