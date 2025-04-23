using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Order
{
    [Key] public int OrderId { get; set; }

    [Required] [ForeignKey("Customer")] public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    [Required] [ForeignKey("Address")] public int AddressId { get; set; }
    public virtual Address Address { get; set; }

    [ForeignKey("OrderReceiptStatus")] public int? OrderReceiptStatusId { get; set; }
    public OrderReceiptStatus OrderReceiptStatus { get; set; }

    /// <summary>
    /// will be false, when the customer refuse to receipt the order.
    /// true when the order is paid
    /// </summary>
    public bool? IsPaid { get; set; }

    public DateTime OrderDate { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}