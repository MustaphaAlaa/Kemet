using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IPriceValidation : IValidator<PriceCreateDTO, PriceUpdateDTO, PriceDeleteDTO> { }
