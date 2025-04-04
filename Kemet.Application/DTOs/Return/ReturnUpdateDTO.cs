namespace Entities.Models.DTOs;

public class ReturnUpdateDTO
{
    public int ReturnId { get; set; }
    public int OrderItemId { get; set; }
    public int ReturnedBy { get; set; }
    public int Quantity { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
}
