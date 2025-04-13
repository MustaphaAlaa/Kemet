using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IPriceValidation : IValidator<PriceCreateDTO, PriceUpdateDTO, PriceDeleteDTO> { }
