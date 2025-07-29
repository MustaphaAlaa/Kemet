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
            .Orders.Where(o => o.ProductId == productId && o.OrderStatusId == orderStatusId)
            .OrderBy(o => o.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Include(o => o.OrderStatus)
            .Include(o => o.Product)
            .Include(o => o.Customer)
            .ThenInclude(c => c.Addresses)
            .ThenInclude(a => a.Governorate)
            .Where(o => o.Customer.Addresses.Any(a => a.IsActive));
    }

    public IQueryable<Order> GetCustomerOrdersInfo(int orderId)
    {
        return _db
            .Orders.Where(o => o.OrderId == orderId)
            .Include(o => o.Customer)
            .Include(o => o.Address)
            .ThenInclude(a => a.Governorate);
    }
}
