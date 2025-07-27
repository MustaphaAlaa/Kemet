using Entities.Enums;
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
                OrderStatusId = (int)enOrderStatus.Pending,
                Name = "معلق",
                Description = "Order is pending.",
            },
            new OrderStatus
            {
                OrderStatusId = (int)enOrderStatus.Processing,
                Name = "جارى المعالجة",
                Description = "Order is being processed.",
            },
            new OrderStatus
            {
                OrderStatusId = (int)enOrderStatus.Shipped,
                Name = "تم الشحن",
                Description = "Order has been shipped.",
            },
            new OrderStatus
            {
                OrderStatusId = (int)enOrderStatus.Delivered,
                Name = "تم التوصيل",
                Description = "Order has been delivered.",
            },
            new OrderStatus
            {
                OrderStatusId = (int)enOrderStatus.CancelledByCustomer,
                Name = "تم الإلغاء من قبل العميل",
                Description = "Order has been cancelled by customer.",
            },
            new OrderStatus
            {
                OrderStatusId = (int)enOrderStatus.CancelledByAdmin,
                Name = "تم الإلغاء من قبل الإدارة",
                Description = "Order has been cancelled by admin.",
            },
            new OrderStatus
            {
                OrderStatusId = (int)enOrderStatus.Refunded,
                Name = "تم استرداد المبلغ",
                Description = "Order has been refunded.",
            },
        };
    }
}
