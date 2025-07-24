 

using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IGovernorateDeliveryCompanyValidation
    : IValidator<
        GovernorateDeliveryCompanyCreateDTO,
        GovernorateDeliveryCompanyUpdateDTO,
        GovernorateDeliveryCompanyDeleteDTO
    > { }