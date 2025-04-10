using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IColorValidation : IValidator<ColorCreateDTO, ColorUpdateDTO, ColorDeleteDTO> { }
