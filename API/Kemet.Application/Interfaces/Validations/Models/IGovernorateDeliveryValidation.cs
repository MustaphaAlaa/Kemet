using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IGovernorateDeliveryValidation
    : IValidator<
        GovernorateDeliveryCreateDTO,
        GovernorateDeliveryUpdateDTO,
        GovernorateDeliveryDeleteDTO
    > { }