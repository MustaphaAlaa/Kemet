namespace Entities.Models.DTOs;

public class ProductQuantityPriceReadDTO
{
    public int ProductQuantityPriceId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public bool IsActive { get; set; }
    public string? Note { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedAt { get; set; }
}
