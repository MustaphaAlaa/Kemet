namespace Kemet.Application.DTOs;

public class ProductQuantityPriceUpdateDTO
{
    public int ProductQuantityPriceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public string? Note { get; set; }
}
