using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class OrderStatusConfigurations : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.HasData(GetOrderStatuses());
    }

    private List<OrderStatus> GetOrderStatuses()
    {
        return new()
        {
            new OrderStatus
            {
                OrderStatusId = 1,
                Name = "Pending",
                Description = "Order is pending.",
            },
            new OrderStatus
            {
                OrderStatusId = 2,
                Name = "Processing",
                Description = "Order is being processed.",
            },
            new OrderStatus
            {
                OrderStatusId = 3,
                Name = "Shipped",
                Description = "Order has been shipped.",
            },
            new OrderStatus
            {
                OrderStatusId = 4,
                Name = "Delivered",
                Description = "Order has been delivered.",
            },
            new OrderStatus
            {
                OrderStatusId = 5,
                Name = "Cancelled By Customer",
                Description = "Order has been cancelled by customer.",
            },
            new OrderStatus
            {
                OrderStatusId = 6,
                Name = "Cancelled By Admin",
                Description = "Order has been cancelled by admin.",
            },
            new OrderStatus
            {
                OrderStatusId = 7,
                Name = "Refunded",
                Description = "Order has been refunded.",
            },
        };
    }
}
