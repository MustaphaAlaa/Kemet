using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IGovernorateService
    : IServiceAsync<
        Governorate,
        int,
        GovernorateCreateDTO,
        GovernorateDeleteDTO,
        GovernorateUpdateDTO,
        GovernorateReadDTO
    >
{
    Task<bool> CheckGovernorateAvailability(int governorateId);
}
