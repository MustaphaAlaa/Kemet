using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class GovernorateDeliveryValidation : IGovernorateDeliveryValidation
{
    private readonly IBaseRepository<GovernorateDelivery> _repository;

    private readonly IValidator<GovernorateDeliveryCreateDTO> _createValidator;
    private readonly IValidator<GovernorateDeliveryUpdateDTO> _updateValidator;
    private readonly IValidator<GovernorateDeliveryDeleteDTO> _deleteValidator;

    public GovernorateDeliveryValidation(
        IBaseRepository<GovernorateDelivery> repository,
        IValidator<GovernorateDeliveryCreateDTO> createValidator,
        IValidator<GovernorateDeliveryUpdateDTO> updateValidator,
        IValidator<GovernorateDeliveryDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(GovernorateDeliveryCreateDTO entity)
    {
        await _createValidator.ValidateAndThrowAsync(entity);
    }

    public async Task ValidateDelete(GovernorateDeliveryDeleteDTO entity)
    {
        await _deleteValidator.ValidateAndThrowAsync(entity);
    }

    public async Task ValidateUpdate(GovernorateDeliveryUpdateDTO entity)
    {
        await _updateValidator.ValidateAndThrowAsync(entity);

        
    }
}
