using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IColorValidation : IValidator<ColorCreateDTO, ColorUpdateDTO, ColorDeleteDTO> { }
