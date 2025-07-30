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

    /// <summary>
    /// Only Active governorates delivery, this for a customer, and showing only active
    /// </summary>
    /// <returns></returns>
    Task<ICollection<GovernorateDeliveryDTO>> ActiveGovernoratesDelivery();

      /// <summary>
    /// Only Active governorate delivery, this for customer's governorate.
    /// </summary>
    /// <returns></returns>
    Task<GovernorateDeliveryReadDTO> ActiveGovernorateDelivery(int governorateId);


    /// <summary>
    /// Shows Active and Governorates with null values, that didn't have value yet.
    /// This Function for admin only
    /// </summary> 
    Task<ICollection<GovernorateDeliveryDTO>> NullableActiveGovernoratesDelivery();

    Task<GovernorateDeliveryReadDTO> DeactivateAndCreate(
        GovernorateDeliveryUpdateDTO updateRequest
    );
}
