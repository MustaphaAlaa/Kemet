using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Return
{
    [Key]
    public int ReturnId { get; set; }

    [ForeignKey("OrderItem")]
    public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }

    [ForeignKey("User")]
    public int ReturnedBy { get; set; }
    public virtual User User { get; set; }

    public int Quantity { get; set; }

    public DateTime ReturnDate { get; set; }
    public string? Notes { get; set; }
    
    public bool HasIssue { get; set; }
    public bool IsRestocked { get; set; }
}
