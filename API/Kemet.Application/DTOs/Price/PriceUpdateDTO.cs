namespace Entities.Models.DTOs;

public class PriceUpdateDTO
{
    public int PriceId { get; set; }
    public int ProductId { get; set; }
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; }
}
