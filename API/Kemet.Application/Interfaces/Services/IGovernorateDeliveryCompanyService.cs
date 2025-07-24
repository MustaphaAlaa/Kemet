using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IGovernorateDeliveryCompanyService
    : IServiceAsync<
        GovernorateDeliveryCompany,
        int,
        GovernorateDeliveryCompanyCreateDTO,
        GovernorateDeliveryCompanyDeleteDTO,
        GovernorateDeliveryCompanyUpdateDTO,
        GovernorateDeliveryCompanyReadDTO
    >
{
    Task<bool> GovernorateDeliveryCompanyAvailability(int deliveryCompanyId, int governorateId);
    Task<GovernorateDeliveryCompanyReadDTO> Deactivate(int governorateDeliveryCompanyId);

    Task AddRange(IEnumerable<GovernorateDeliveryCompanyCreateDTO> entities);
    Task<GovernorateDeliveryCompanyReadDTO> SoftUpdate(
        GovernorateDeliveryCompanyUpdateDTO updateRequest
    );
}
