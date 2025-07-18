using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class ReturnStatusConfigurations : IEntityTypeConfiguration<ReturnStatus>
{
    public void Configure(EntityTypeBuilder<ReturnStatus> builder)
    {
        builder.HasMany(ri => ri.ReturnItems).WithOne().HasForeignKey(ri => ri.ReturnStatusId);
        builder.HasData(GetReturnStatuses());
    }

    private List<ReturnStatus> GetReturnStatuses()
    {
        return new()
        {
            new ReturnStatus
            {
                ReturnStatusId = 1,
                Name = "With Delivery Company",
                Description = "Delivery person has the items",
            },
            new ReturnStatus
            {
                ReturnStatusId = 2,
                Name = "In Transit",
                Description = "Delivery company bringing items back",
            },
            new ReturnStatus
            {
                ReturnStatusId = 3,
                Name = "Received",
                Description = "Items physically returned to the business.",
            },
            new ReturnStatus
            {
                ReturnStatusId = 4,
                Name = "Under Inspection",
                Description = "Checking item condition.",
            },
            new ReturnStatus
            {
                ReturnStatusId = 5,
                Name = "Restocked",
                Description = "Return item is restocked.",
            },
            new ReturnStatus
            {
                ReturnStatusId = 6,
                Name = "Disposed",
                Description = "Return item is disposed.",
            },
            new ReturnStatus
            {
                ReturnStatusId = 7,
                Name = "Lost",
                Description = "Return item is lost.",
            },
        };
    }
}
