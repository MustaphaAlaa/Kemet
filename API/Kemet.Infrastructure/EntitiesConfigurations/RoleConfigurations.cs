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
                Name = "Admin",
                NormalizedName = "ADMIN",
            },
            new Role
            {
                Id = Guid.Parse("6c8f1b93-a7f0-43ac-ba78-926940731a3c"),
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
            },
            new Role
            {
                Id = Guid.Parse("6e76d29d-eb6d-475d-87b1-6aaa614b3a32"),
                Name = "Customer",
                NormalizedName = "Customer",
            }
        );
    }
}
