namespace Entities.Models.DTOs;

// Return DTOs
public class ReturnCreateDTO
{
    public int OrderItemId { get; set; }
    public int ReturnedBy { get; set; }
    public int Quantity { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
}
