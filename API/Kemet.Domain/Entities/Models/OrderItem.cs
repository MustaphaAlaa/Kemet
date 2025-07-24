using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class OrderItem
{
    [Key]
    public int OrderItemId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    [ForeignKey("ProductVariant")]
    public int ProductVariantId { get; set; }
    public virtual ProductVariant ProductVariant { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}
