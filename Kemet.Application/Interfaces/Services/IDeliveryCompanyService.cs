using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IDeliveryCompanyService
    : IServiceAsync<
        DeliveryCompany,
        int,
        DeliveryCompanyCreateDTO,
        DeliveryCompanyDeleteDTO,
        DeliveryCompanyUpdateDTO,
        DeliveryCompanyReadDTO
    >
{
    Task<bool> CheckDeliveryCompanyAvailability(int deliveryCompanyId);
    Task<DeliveryCompany> CreateWithTrackingAsync(DeliveryCompanyCreateDTO entity);

    Task<IEnumerable<GovernorateDeliveryCompanyDTO>> ActiveGovernorates(int deliveryCompanyId);
}
