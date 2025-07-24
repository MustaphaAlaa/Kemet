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

    public bool IsActive { get; set; } = true;

    [Required]
    public DateTime DialingWithItFrom { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<GovernorateDeliveryCompany> GovernoratesDeliveryCompany { get; set; }
}
