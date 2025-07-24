using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Return
{
    [Key]
    public int ReturnId { get; set; }

    [ForeignKey("User")]
    public int HandledByUserId { get; set; }
    public virtual User HandledByUser { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual required Order Order { get; set; }

    public int ReturnedQuantity { get; set; }
    public decimal TotalReturnAmount { get; set; } // should be calculated based on the ReturnItem records
    public decimal TotalRefundedAmount { get; set; } // should be calculated based on refund transactions
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Notes { get; set; }
    public bool IsReturnFullyRefunded => TotalRefundedAmount >= TotalReturnAmount;
    public ICollection<ReturnItem> ReturnItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
