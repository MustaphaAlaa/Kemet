using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs; 

namespace IServices;

public interface IGovernorateDeliveryService
    : IServiceAsync<
        GovernorateDelivery,
        int,
        GovernorateDeliveryCreateDTO,
        GovernorateDeliveryDeleteDTO,
        GovernorateDeliveryUpdateDTO,
        GovernorateDeliveryReadDTO
    >
{
    Task<bool> CheckGovernorateDeliveryAvailability(int governorateId);
}
