using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ProductVariant
{
    [Key]
    public int ProductVariantId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    [ForeignKey("Color")]
    public int ColorId { get; set; }
    public virtual Color Color { get; set; }

    [ForeignKey("Size")]
    public int SizeId { get; set; }
    public virtual Size Size { get; set; }

    [Required]
    public int Stock { get; set; }
}
