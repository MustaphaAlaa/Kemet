using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ReturnItem
{
    [Key]
    public int OrderItemReturnsId { get; set; }

    [ForeignKey("OrderItem")]
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }

    [ForeignKey("Return")]
    public int ReturnId { get; set; }
    public virtual Return Return { get; set; }

    [ForeignKey("ReturnStatus")]
    public int ReturnStatusId { get; set; }
    public virtual ReturnStatus ReturnStatus { get; set; }

    public string? Notes { get; set; }

    public short RefundAmount { get; set; } // Amount to be refunded for this return
    public short RefundedAmount { get; set; } // Amount of refund is already back to the business
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}
