using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface ICustomerValidation
    : IValidator<CustomerCreateDTO, CustomerUpdateDTO, CustomerDeleteDTO> { }
