using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class RoleConfigurations : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Id = Guid.Parse("a5ac81fe-cfe3-4d1b-8be8-ed37cc86c434"),
                Name = Roles.Admin,
                NormalizedName = Roles.Admin.ToUpper(),
            },
            new Role
            {
                Id = Guid.Parse("6c8f1b93-a7f0-43ac-ba78-926940731a3c"),
                Name = Roles.Employee,
                NormalizedName = Roles.Employee.ToUpper(),
            },
            new Role
            {
                Id = Guid.Parse("6e76d29d-eb6d-475d-87b1-6aaa614b3a32"),
                Name = Roles.Customer,
                NormalizedName = Roles.Customer.ToUpper(),
            }
        );
    }
}
