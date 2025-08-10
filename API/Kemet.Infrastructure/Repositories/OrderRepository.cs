using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities;
using Entities.Infrastructure;
using Entities.Infrastructure.Extensions;
using Entities.Models;
using IRepository;
using IRepository.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Generic;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
 
    private readonly KemetDbContext _db;

    public OrderRepository(KemetDbContext context)
        : base(context)
    {
        _db = context;
    }

    public async Task<PaginatedResult<Order>> GetOrdersForItsStatus(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    )
    {
        var orders = await GetOrdersWithIncludes(order =>
                order.ProductId == productId && order.OrderStatusId == orderStatusId
            )
            .ToPaginateListAsync(pageNumber, pageSize);

        return orders;
    }

    public async Task<PaginatedResult<Order>> GetOrdersForDeliveryCompany(
        int deliveryCompanyId,
        int pageNumber = 1,
        int pageSize = 50
    )
    {
        var orders = await GetOrdersWithIncludes(order =>
                order.DeliveryCompanyId == deliveryCompanyId
            )
            .ToPaginateListAsync(pageNumber, pageSize);

        return orders;
    }

    public IQueryable<Order> GetOrdersWithIncludes(Expression<Func<Order, bool>> expression)
    {
        var orders = _db
            .Orders.Where(expression)
            .OrderBy(order => order.CreatedAt)
            .Include(order => order.OrderStatus)
            .Include(order => order.Product)
            .Include(order => order.GovernorateDelivery)
            .Include(order => order.GovernorateDeliveryCompany)
            .Include(order => order.ProductQuantityPrice)
            .Include(order => order.GovernorateDelivery)
            .Include(order => order.Customer)
            .ThenInclude(c => c.Addresses)
            .ThenInclude(a => a.Governorate)
            .Where(order => order.Customer.Addresses.Any(a => a.IsActive));

        return orders;
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
