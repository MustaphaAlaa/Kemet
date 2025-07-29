using Entities.Infrastructure;
using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Generic;

public class OrderItemRepository : RangeRepository<OrderItem>, IOrderItemRepository
{
    private readonly KemetDbContext _db;

    public OrderItemRepository(KemetDbContext context)
        : base(context)
    {
        _db = context;
    }

    public IQueryable<OrderItem> GetOrderItemsForOrder(int orderId)
    {
        return _db
            .OrderItems.Include(orderItem => orderItem.ProductVariant)
            .ThenInclude(productVariant => productVariant.Color)
            .Include(orderItem => orderItem.ProductVariant)
            .ThenInclude(productVariant => productVariant.Size)
            .Where(orderItem => orderItem.OrderId == orderId);
    }
}
