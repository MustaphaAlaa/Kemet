using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(this.Categories());
    }


    private List<Category> Categories()
    {
        List<Category> categories = new();
        int Ids = 0;
        categories.Add(new Category { CategoryId = 1, Name = "بناطيل جبردين" });
        categories.Add(new Category { CategoryId = 2, Name = "بناطيل جينز" });
        categories.Add(new Category { CategoryId = 3, Name = "جيبة جينز" });


        return categories;
    }
}
