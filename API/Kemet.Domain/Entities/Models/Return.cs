using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Return
{
    [Key]
    public int ReturnId { get; set; }

    [ForeignKey("User")]
    public int HandledBy { get; set; }
    public virtual User User { get; set; }

    public int Quantity { get; set; }
    public decimal TotalReturnsPrice { get; set; } // should be calculated based on the OrderItemReturn records
    public decimal TotalRefundedPrice { get; set; } // should be calculated based on
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? Notes { get; set; }
    public bool IsFullyRefunded => TotalRefundedPrice >= TotalReturnsPrice; 
    public ICollection<ReturnItem> ReturnItems { get; set; }
}
