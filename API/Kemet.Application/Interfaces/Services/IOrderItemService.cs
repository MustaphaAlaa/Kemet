using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderItemService
    : IServiceAsync<
        OrderItem,
        int,
        OrderItemCreateDTO,
        OrderItemDeleteDTO,
        OrderItemUpdateDTO,
        OrderItemReadDTO
    >
{
    Task<OrderItem> CreateWithTrackingAsync(OrderItemCreateDTO entity);
    Task<ICollection<OrderItemWithProductVariantData>> GetOrderItemsForOrder(int orderId);
}
