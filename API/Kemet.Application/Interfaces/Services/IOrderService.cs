using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderService
    : IServiceAsync<Order, int, OrderCreateDTO, OrderDeleteDTO, OrderUpdateDTO, OrderReadDTO>
{
    Task<Order> CreateWithTrackingAsync(OrderCreateDTO entity);
    Task<ICollection<OrderInfoDTO>> GetOrdersForItsStatusAsync(
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
}
