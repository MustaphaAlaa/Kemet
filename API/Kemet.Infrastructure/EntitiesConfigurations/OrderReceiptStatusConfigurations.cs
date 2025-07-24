using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class OrderReceiptStatusConfigurations : IEntityTypeConfiguration<OrderReceiptStatus>
{
    public void Configure(EntityTypeBuilder<OrderReceiptStatus> builder)
    {
        builder.HasKey(x => x.OrderReceiptStatusId);
        builder.HasData(GetOrderReceiptStatuses());
    }

    private IList<OrderReceiptStatus> GetOrderReceiptStatuses()
    {
        return new List<OrderReceiptStatus>
        {
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.FullyReceipt,
                Name = "Fully Receipt",
            },
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.PartiallyReceipt,
                Name = "Partially Receipt",
            },
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.RefusedReceipt,
                Name = "Refused Receipt",
            },
            new OrderReceiptStatus
            {
                OrderReceiptStatusId = (int)enOrderReceiptStatus.AttemptFailed,
                Name = "Attempt Failed",
            },
        };
    }
}
