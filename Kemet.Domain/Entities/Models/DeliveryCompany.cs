using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

/// <summary>
/// Delivery Company Data
/// </summary>
public class DeliveryCompany
{
    [Key]
    public int DeliveryCompanyId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public DateTime DialingWithItFrom { get; set; }
}
