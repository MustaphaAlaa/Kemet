using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

/// <summary>
/// Delivery cost for governorates that customer should pay it.
/// </summary>
public class GovernorateDelivery
{
    [Key]
    public int GovernorateDeliveryId { get; set; }

    public decimal? DeliveryCost { get; set; }

    public bool? IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    [ForeignKey("Governorate")]
    public int GovernorateId { get; set; }
    public virtual Governorate Governorate { get; set; }
}
