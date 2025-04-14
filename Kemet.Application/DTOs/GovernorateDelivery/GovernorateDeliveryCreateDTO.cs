using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DTOs.GovernorateDelivery;

public class GovernorateDeliveryCreateDTO
{
    public string CompanyName { get; set; }

    public decimal DeliveryCost { get; set; }

    public int GovernorateId { get; set; }
}
