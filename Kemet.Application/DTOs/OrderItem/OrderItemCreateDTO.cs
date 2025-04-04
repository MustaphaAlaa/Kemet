namespace Entities.Models.DTOs;

// OrderItem DTOs
public class OrderItemCreateDTO
{
    public int ProductVariantId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
