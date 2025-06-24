using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Entities.Infrastructure.EntitiesConfigurations;

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
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "اسود",
                HexaCode = "#000000",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "كحلى",
                HexaCode = "#000080",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "بنى غامق",
                HexaCode = "#654321",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "رمادى غامق",
                HexaCode = "#A9A9A9",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "زيتونى",
                HexaCode = "#808000",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "فضي",
                HexaCode = "#C0C0C0",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "جملي",
                HexaCode = "#C19A6B",
            }
        );
        colors.Add(
            new Color
            {
                ColorId = ++Ids,
                Name = "بيج",
                HexaCode = "#F5F5DC",
            }
        );
        return colors;
    }
}
