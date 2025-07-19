using Entities.Enums;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class DeliveryPaymentStatusConfigurations : IEntityTypeConfiguration<DeliveryPaymentStatus>
{
    public void Configure(EntityTypeBuilder<DeliveryPaymentStatus> builder)
    {
        builder.HasKey(x => x.DeliveryPaymentStatusId);
        builder.HasData(GetDeliveryPaymentStatuses());
    }

    private IList<DeliveryPaymentStatus> GetDeliveryPaymentStatuses()
    {
        return new List<DeliveryPaymentStatus>
        {
            new DeliveryPaymentStatus
            {
                DeliveryPaymentStatusId = (int)enDeliveryPaymentStatus.DeliveryFullPaid,
                Name = "Full Paid",
                Description = "Delivery cost has been fully collected.",
            },
            new DeliveryPaymentStatus
            {
                DeliveryPaymentStatusId = (int)enDeliveryPaymentStatus.DeliveryPartiallyPaid,
                Name = "Partially Paid",
                Description = "Only part of the delivery cost has been collected.",
            },
            new DeliveryPaymentStatus
            {
                DeliveryPaymentStatusId = (int)enDeliveryPaymentStatus.DeliveryPending,
                Name = "Pending",
                Description = "Delivery cost is pending; payment has not been fully collected.",
            },
            new DeliveryPaymentStatus
            {
                DeliveryPaymentStatusId = (int)enDeliveryPaymentStatus.DeliveryUnpaid,
                Name = "Unpaid",
                Description = "Delivery cost has not been collected at all.",
            },
        };
    }
}
