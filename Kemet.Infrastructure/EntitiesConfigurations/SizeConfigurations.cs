using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kemet.Infrastructure.EntitiesConfigurations;

public class SizeConfigurations : IEntityTypeConfiguration<Size>
{
    public void Configure(EntityTypeBuilder<Size> builder)
    {
        builder.HasData(this.Sizes());
    }


    //Sizes
    //30-32-34-36-38-40-42-44-46-48-50

    private List<Size> Sizes()
    {
        List<Size> sizes = new(15);

        int Ids = 1;

        for (int size = 30; size < 52; size += 2, Ids++)
            sizes.Add(new Size { SizeId = Ids, Name = size.ToString() });

        sizes.Add(new Size { SizeId = Ids, Name = "S" });
        sizes.Add(new Size { SizeId = ++Ids, Name = "M" });
        sizes.Add(new Size { SizeId = ++Ids, Name = "L" });
        sizes.Add(new Size { SizeId = ++Ids, Name = "XL" });
        sizes.Add(new Size { SizeId = ++Ids, Name = "XXL" });
        sizes.Add(new Size { SizeId = ++Ids, Name = "XXXL" });

        return sizes;

    }
}
