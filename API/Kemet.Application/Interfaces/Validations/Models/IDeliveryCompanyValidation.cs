using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IDeliveryCompanyValidation
    : IValidator<DeliveryCompanyCreateDTO, DeliveryCompanyUpdateDTO, DeliveryCompanyDeleteDTO> { }
