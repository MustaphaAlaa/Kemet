namespace Kemet.Application.DTOs;

public class PriceReadDTO
{
    public int PriceId { get; set; }
    public decimal MininmumPrice { get; set; }
    public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
}
