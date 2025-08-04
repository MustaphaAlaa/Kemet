using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IOrderValidation : IValidator<OrderCreateDTO, OrderUpdateDTO, OrderDeleteDTO>
{
    Task ValidateUpdateOrderStatus(int orderStatusId);
    Task ValidateUpdateOrderReceiptStatus(int orderReceiptStatusId);
    Task ValidateUpdateOrderDeliveryCompany(Order order, int deliveryCompanyId, int governorateId);
    void ValidateUpdateOrderNote(Order order, string note);

    Task UpdateCodeForDeliveryCompany(Order order, int orderId, string deliveryCompanyCode);

}
