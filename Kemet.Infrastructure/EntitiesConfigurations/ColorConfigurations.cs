using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemet.Infrastructure.EntitiesConfigurations;

public class ColorConfigurations : IEntityTypeConfiguration<Color>
{
    public void Configure(EntityTypeBuilder<Color> builder)
    {
        builder.HasData(this.Colors());
    }

    // Sizes 
    // اسود كحلي بني غامق فيراني زيتوني فضي جملي بيج
    private List<Color> Colors()
    {
        List<Color> colors = new();
        int Ids = 0;
        colors.Add(new Color { ColorId = ++Ids, NameAr = "اسود", NameEn = "Black" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "كحلى", NameEn = "Navy" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "بنى غامق", NameEn = "Dark Brown" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "رمادى غامق", NameEn = "Drak Grey" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "زيتونى", NameEn = "Olive" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "فضي", NameEn = "Sliver" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "جملي", NameEn = "Camel / Light Brown" });
        colors.Add(new Color { ColorId = ++Ids, NameAr = "بيج", NameEn = "Beige" });
        return colors;

    }
}