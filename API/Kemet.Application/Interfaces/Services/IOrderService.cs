using Domain.IServices;
using Entities;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderService
    : IServiceAsync<Order, int, OrderCreateDTO, OrderDeleteDTO, OrderUpdateDTO, OrderReadDTO>
{
    Task<Order> CreateWithTrackingAsync(OrderCreateDTO entity);
    Task<PaginatedResult<OrderInfoDTO>> GetOrdersForItsStatusAsync(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    );
    Task<ICollection<OrderStatusReadDTO>> GetOrderStatusesAsync();
    Task<OrderReadDTO> UpdateOrderStatus(int orderId, int orderStatusId);
    Task<OrderReadDTO> UpdateOrderReceiptStatus(
        int orderId,
        int orderReceiptStatusId,
        string note = ""
    );

    Task<GetCustomerOrdersInfo> GetCustomerOrdersInfo(int orderId);
    Task<Order> UpdateOrderDeliveryCompany(int orderId, int deliveryCompanyId, int governorateId);


    Task<Order> UpdateOrderGovernorateDeliveryCompany(int orderId, int governorateDeliveryCompany);
}
