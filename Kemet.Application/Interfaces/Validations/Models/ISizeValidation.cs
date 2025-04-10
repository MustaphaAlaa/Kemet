using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface ISizeValidation : IValidator<SizeCreateDTO, SizeUpdateDTO, SizeDeleteDTO> { }
