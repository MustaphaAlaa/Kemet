using System.ComponentModel.DataAnnotations;

namespace Kemet.Application.DTOs.GovernorateDelivery;

public class GovernorateDeliveryCreateDTO
{
    public string CompanyName { get; set; }

    public decimal DeliveryCost { get; set; }

    public int GovernorateId { get; set; }
}
