namespace Entities.Models.DTOs;

public class OrderUpdateDTO
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemUpdateDTO> OrderItems { get; set; }
}
