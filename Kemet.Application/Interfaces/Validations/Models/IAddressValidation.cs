using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IAddressValidation
    : IValidator<AddressCreateDTO, AddressUpdateDTO, AddressDeleteDTO> { }
