namespace Entities.Models.DTOs;

public class ProductVariantUpdateDTO
{
    public int ProductVariantId { get; set; }
    public int ProductId { get; set; }
    public int ColorId { get; set; }
    public int SizeId { get; set; }
    public int Stock { get; set; }
}
