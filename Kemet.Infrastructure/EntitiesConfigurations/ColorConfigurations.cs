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
        colors.Add(new Color { ColorId = ++Ids, Name = "اسود", NameEn = "Black", Hexacode = "#000000" });
        colors.Add(new Color { ColorId = ++Ids, Name = "كحلى", NameEn = "Navy", Hexacode = "#000080" });
        colors.Add(new Color { ColorId = ++Ids, Name = "بنى غامق", NameEn = "Dark Brown", Hexacode = "#654321" });
        colors.Add(new Color { ColorId = ++Ids, Name = "رمادى غامق", NameEn = "Drak Grey", Hexacode = "#A9A9A9" });
        colors.Add(new Color { ColorId = ++Ids, Name = "زيتونى", NameEn = "Olive", Hexacode = "#808000" });
        colors.Add(new Color { ColorId = ++Ids, Name = "فضي", NameEn = "Sliver", Hexacode = "#C0C0C0" });
        colors.Add(new Color { ColorId = ++Ids, Name = "جملي", NameEn = "Camel / Light Brown", Hexacode = "#C19A6B" });
        colors.Add(new Color { ColorId = ++Ids, Name = "بيج", NameEn = "Beige", Hexacode = "#F5F5DC" });
        return colors;
    }
}
