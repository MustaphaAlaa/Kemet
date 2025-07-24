using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IReturnValidation
    : IValidator<ReturnCreateDTO, ReturnUpdateDTO, ReturnDeleteDTO> { }
