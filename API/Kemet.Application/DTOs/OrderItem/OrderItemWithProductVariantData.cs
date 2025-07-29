namespace Entities.Models.DTOs;

public class OrderItemWithProductVariantData
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductVariantId { get; set; }
    public string Color { get; set; }
    public string Size { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
