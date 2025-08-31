using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class OrderConfigurations : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasMany(Items => Items.OrderItems)
            .WithOne(Item => Item.Order)
            .HasForeignKey(Item => Item.OrderId);

        builder.HasIndex(order => order.CodeFromDeliveryCompany);
    }
}
