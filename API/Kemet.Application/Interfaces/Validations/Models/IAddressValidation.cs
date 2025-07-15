using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IAddressValidation
    : IValidator<AddressCreateDTO, AddressUpdateDTO, AddressDeleteDTO> { }
