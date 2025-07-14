using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

/// <summary>
/// For Payments, and its type, which order its cash is collected
/// </summary>
public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [Required]
    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    [Required]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    [Required]
    [ForeignKey("PaymentType")]
    public int PaymentTypeId { get; set; }
    public virtual PaymentType PaymentType { get; set; }
}
