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
}
