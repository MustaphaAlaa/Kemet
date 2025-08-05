namespace Entities.Models.DTOs;

public class OrderReadDTO
{
    public int? OrderId { get; set; }
    public Guid? CustomerId { get; set; }
    public int? OrderReceiptStatusId { get; set; }
    public int? OrderStatusId { get; set; }
    public string? Note { get; set; }
    public string? CodeFromDeliveryCompany { get; set; }
    public DateTime? OrderDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public List<OrderItemReadDTO>? OrderItems { get; set; }
}
