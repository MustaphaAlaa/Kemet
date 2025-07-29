using Entities.Models;
using IRepository.Generic;

namespace IRepository;

public interface IOrderItemRepository : IRangeRepository<OrderItem>
{
    public IQueryable<OrderItem> GetOrderItemsForOrder(int orderId);

   
}
