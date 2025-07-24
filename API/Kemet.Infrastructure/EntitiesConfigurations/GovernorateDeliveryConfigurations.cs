using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class GovernorateDeliveryConfigurations : IEntityTypeConfiguration<GovernorateDelivery>
{
    public void Configure(EntityTypeBuilder<GovernorateDelivery> builder)
    {
        builder.HasOne(g => g.Governorate);
        builder.HasData(GetGovernoratesDelivery());
    }

    private List<GovernorateDelivery> GetGovernoratesDelivery()
    {
        byte id = 1;
        return new List<GovernorateDelivery>
        {
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
            new GovernorateDelivery
            {
                GovernorateId = id++,
                GovernorateDeliveryId = id,
                CreatedAt = new DateTime(),
                DeliveryCost = null,
                IsActive = null,
            },
        };
    }
}
