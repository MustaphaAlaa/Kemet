using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

/// <summary>
/// for store Payments types
/// </summary>
public class PaymentType
{
    [Key]
    public int PaymentTypeId { get; set; }

    [Required]
    public string Name { get; set; }
}
