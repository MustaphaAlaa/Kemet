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

    Task<OrderStatus_OrderReceipt> UpdateOrderStatus(
        OrderStatus_OrderReceipt orderStatus_OrderReceipt
    );

    Task<OrderStatus_OrderReceipt> UpdateOrderReceiptStatus(
        OrderStatus_OrderReceipt orderStatus_OrderReceipt
    );

    Task<GetCustomerOrdersInfo> GetCustomerOrdersInfo(int orderId);
    Task<Order> UpdateOrderDeliveryCompany(int orderId, int deliveryCompanyId, int governorateId);

    Task<Order> UpdateOrderGovernorateDeliveryCompany(int orderId, int governorateDeliveryCompany);
    Task<OrderReadDTO> UpdateOrderNote(int orderId, string note);
    Task<OrderReadDTO> UpdateCodeForDeliveryCompany(int orderId, string DeliveryCompanyCode);
    
    Task<PaginatedResult<OrderInfoDTO>> GetOrdersForDeliveryCompany(
        int deliveryCompanyId,
        int pageNumber = 1,
        int pageSize = 50
    );
}
