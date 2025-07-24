using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class DeliveryTransaction
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }
    public virtual Order Order { get; set; }

    [ForeignKey("GovernorateDeliveryCompany")]
    public int GovernorateDeliveryCompanyId { get; set; }
    public virtual GovernorateDeliveryCompany GovernorateDeliveryCompany { get; set; }

    public decimal Difference { get; set; }

    public DateTime CreatedAt { get; set; }  
}
