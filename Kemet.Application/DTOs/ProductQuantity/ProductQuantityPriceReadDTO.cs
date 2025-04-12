namespace Kemet.Application.DTOs;

public class ProductQuantityPriceReadDTO
{
    public int ProductQuantityPriceId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public string? Note { get; set; }
    public int ProductId { get; set; }

}
