using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface ICustomerValidation
    : IValidator<CustomerCreateDTO, CustomerUpdateDTO, CustomerDeleteDTO> { }
