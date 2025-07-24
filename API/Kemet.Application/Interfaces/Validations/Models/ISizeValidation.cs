using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface ISizeValidation : IValidator<SizeCreateDTO, SizeUpdateDTO, SizeDeleteDTO> { }
