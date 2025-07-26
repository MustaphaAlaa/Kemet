using Entities.Models;
using IRepository.Generic;

namespace IRepository;

public interface IOrderRepository : IBaseRepository<Order>
{
    public IQueryable<Order> GetOrdersForItsStatus(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    );
}
