using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

/// <summary>
/// Deliver cost for governorates for each country 
/// </summary>
public class GovernorateDeliveryCompany
{
    [Key] public int GovernorateDeliveryCompanyId { get; set; }

    [ForeignKey("DeliveryCompany")] public int DeliveryCompanyId { get; set; }
    public virtual DeliveryCompany DeliveryCompany { get; set; }

    [ForeignKey("Governorate")] public int GovernorateId { get; set; }
    public virtual Governorate Governorate { get; set; }

    [Required] public decimal DeliveryCost { get; set; }

    [Required] public DateTime CreatedAt { get; set; }
}