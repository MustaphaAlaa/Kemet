namespace Entities.Models.DTOs;

public class PriceCreateDTO
{
    public decimal MinimumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; } = true;
    public int ProductId { get; set; }
}
