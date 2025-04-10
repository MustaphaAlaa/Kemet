using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IReturnValidation
    : IValidator<ReturnCreateDTO, ReturnUpdateDTO, ReturnDeleteDTO> { }
