using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class GovernorateDelivery
{
    [Key]
    public int DeliveryId { get; set; }

    [Required]
    public string CompanyName { get; set; }

    [Required]
    public decimal DeliveryCost { get; set; }

    [ForeignKey("Governorate")]
    public int GovernorateId { get; set; }
    public virtual Governorate Governorate { get; set; }
}
