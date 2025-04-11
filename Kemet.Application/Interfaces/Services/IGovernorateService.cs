using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IGovernorateService
    : IServiceAsync<
        Governorate,
        GovernorateCreateDTO,
        GovernorateDeleteDTO,
        GovernorateUpdateDTO,
        GovernorateReadDTO
    > { }
