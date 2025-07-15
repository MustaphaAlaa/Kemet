namespace Entities.Models.DTOs;

public class OrderReadDTO
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public int? OrderReceiptStatusId { get; set; }
    public int OrderStatusId { get; set; }
    public bool IsPaid { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemReadDTO> OrderItems { get; set; }
}
