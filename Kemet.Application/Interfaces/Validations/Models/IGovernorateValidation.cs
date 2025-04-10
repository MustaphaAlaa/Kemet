using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IGovernorateValidation
    : IValidator<GovernorateCreateDTO, GovernorateUpdateDTO, GovernorateDeleteDTO> { }
