namespace Entities.Models.DTOs;

// Order DTOs
public class OrderCreateDTO
{

    public int CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemCreateDTO> OrderItems { get; set; }

}
