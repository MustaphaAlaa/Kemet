namespace Entities.Models.DTOs;

// IProductVariantServices DTOs
public class ProductVariantCreateDTO
{
    public int ProductId { get; set; }
    public int ColorId { get; set; }
    public int SizeId { get; set; }
    public int Stock { get; set; }
}
