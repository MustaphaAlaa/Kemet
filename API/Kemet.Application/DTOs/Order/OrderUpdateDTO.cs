namespace Entities.Models.DTOs;

public class OrderUpdateDTO
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }

    public int OrderStatusId { get; set; }


    /// <summary>
    /// null when the order didn't receipt yet.
    /// if order is receipt, it'll take  value from OrderReceiptStatus table/Enum. 
    /// </summary>
    public int? OrderReceiptStatusId { get; set; }


    /// <summary>
    /// will be false, when the customer refuse to receipt the order.
    /// true when the order is paid.
    /// null when the order didn't receipt yet.
    /// </summary>
    public bool? IsPaid { get; set; }
    public List<OrderItemUpdateDTO> OrderItems { get; set; }
}
