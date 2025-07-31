using Entities;
using Entities.Models;
using IRepository.Generic;

namespace IRepository;

public interface IOrderRepository : IBaseRepository<Order>
{
    public Task<PaginatedResult<Order>> GetOrdersForItsStatus(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    );

    IQueryable<Order> GetCustomerOrdersInfo(int orderId);
}
