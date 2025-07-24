namespace Entities.Models.DTOs;

public class ProductVariantReadWithDetailsDTO
{
    public int ProductVariantId { get; set; }
    public int ProductId { get; set; }
    public ColorReadDTO Color { get; set; }
    public SizeReadDTO Size { get; set; }
    public int StockQuantity { get; set; }
}
