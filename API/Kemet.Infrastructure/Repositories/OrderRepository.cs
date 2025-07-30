using Entities.Infrastructure;
using Entities.Models;
using IRepository;
using IRepository.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Generic;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    // protected readonly KemetDbContext _db;
    private readonly KemetDbContext _db;

    public OrderRepository(KemetDbContext context)
        : base(context)
    {
        _db = context;
    }

    public IQueryable<Order> GetOrdersForItsStatus(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    )
    {
        return _db
            .Orders.Where(order =>
                order.ProductId == productId && order.OrderStatusId == orderStatusId
            )
            .OrderBy(order => order.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(order => order.OrderStatus)
            .Include(order => order.Product)
            .Include(order => order.GovernorateDelivery)
            .Include(order => order.Customer)
            .ThenInclude(c => c.Addresses)
            .ThenInclude(a => a.Governorate)
            .Where(order => order.Customer.Addresses.Any(a => a.IsActive));
    }

    public IQueryable<Order> GetCustomerOrdersInfo(int orderId)
    {
        return _db
            .Orders.Where(order => order.OrderId == orderId)
            .Include(order => order.Customer)
            .Include(order => order.Address)
            .ThenInclude(address => address.Governorate);
    }
}
