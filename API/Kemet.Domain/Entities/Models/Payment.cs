using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Enums;

namespace Entities.Models;

/// <summary>
/// Represents a payment collected for an order, including the amount paid and the delivery cost paid.
/// The net payment for the order is calculated by subtracting the delivery cost paid from the total amount paid.
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
    public decimal PaidAmount { get; set; }

    // public decimal PaidForOrder { get; set; }
    // public decimal PaidForDeliveryCost { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    [Required]
    [ForeignKey("PaymentType")]
    public int PaymentTypeId { get; set; }
    public virtual PaymentType PaymentType { get; set; }
}
