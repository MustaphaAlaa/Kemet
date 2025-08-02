using Entities.Models.DTOs;

namespace IServices.Orchestrator;

public interface IUpdateOrderOrchestratorService
{
    Task<DeliveryCompanyDetailsDTO> UpdateDeliveryCompanyForOrder(
        int orderId,
        int deliveryCompanyId,
        int governorateId
    );
}
