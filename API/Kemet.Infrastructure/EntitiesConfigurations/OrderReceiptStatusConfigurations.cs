using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class OrderReceiptStatusConfigurations : IEntityTypeConfiguration<OrderReceiptStatus>
{
    public void Configure(EntityTypeBuilder<OrderReceiptStatus> builder)
    {
  
        builder.HasData(GetOrderReceiptStatuses());
    }

    private IList<OrderReceiptStatus> GetOrderReceiptStatuses()
    {
        return new List<OrderReceiptStatus>
        {
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.FullyReceipt,
                Name = "تم الاستلام بالكامل",
            },
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.PartiallyReceipt,
                Name = "تم الاستلام جزئيًا",
            },
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.RefusedReceipt,
                Name = "تم رفض الاستلام",
            },
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.DeliveryAttemptFailed,
                Name = "فشل محاولة التسليم",
            },
        };
    }
}
