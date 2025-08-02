using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IOrderValidation : IValidator<OrderCreateDTO, OrderUpdateDTO, OrderDeleteDTO>
{
    Task ValidateUpdateOrderStatus(int orderStatusId);
    Task ValidateUpdateOrderReceiptStatus(int orderReceiptStatusId);
    Task ValidateUpdateOrderDeliveryCompany(int deliveryCompanyId, int governorateId);
}
