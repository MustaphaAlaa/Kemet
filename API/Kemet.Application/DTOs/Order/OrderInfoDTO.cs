namespace Entities.Models.DTOs;

public class OrderInfoDTO
{
    public string CustomerName { get; set; }
    public string GovernorateName { get; set; }
    public string StreetAddress { get; set; }

    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int OrderStatusId { get; set; }
    public int? OrderReceiptStatusId { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal GovernorateDeliveryCost { get; set; }
    public int GovernorateId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
