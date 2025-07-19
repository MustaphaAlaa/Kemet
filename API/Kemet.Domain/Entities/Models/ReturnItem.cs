using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ReturnItem
{
    [Key]
    public int ReturnItemId { get; set; }

    [ForeignKey("OrderItem")]
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }

    [ForeignKey("Return")]
    public int ReturnId { get; set; }
    public virtual Return Return { get; set; }

    [ForeignKey("ReturnStatus")]
    public int ReturnStatusId { get; set; }
    public virtual ReturnStatus ReturnStatus { get; set; }

    public string? ReturnNotes { get; set; }

    public short RequestedRefundAmount { get; set; } // Amount requested to be refunded for this return
    public short ProcessedRefundAmount { get; set; } // Amount of refund already processed back to the business
    public decimal ItemUnitPrice { get; set; }
    public decimal ItemTotalPrice { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}
