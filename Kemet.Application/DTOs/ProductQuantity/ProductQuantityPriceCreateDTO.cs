namespace Kemet.Application.DTOs;

public class ProductQuantityPriceCreateDTO
{
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; } = true;
    public string? Note { get; set; }
    public int ProductId { get; set; }
}
