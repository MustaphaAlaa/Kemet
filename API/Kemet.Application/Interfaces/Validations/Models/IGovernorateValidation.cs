using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IGovernorateValidation
    : IValidator<GovernorateCreateDTO, GovernorateUpdateDTO, GovernorateDeleteDTO> { }
