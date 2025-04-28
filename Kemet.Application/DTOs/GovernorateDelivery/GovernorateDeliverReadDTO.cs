using System.Reflection.Metadata;

namespace Entities.Models.DTOs;

public class GovernorateDeliveryReadDTO
{
    public int GovernorateDeliveryId { get; set; }

    public decimal DeliveryCost { get; set; }

    public int GovernorateId { get; set; }

    public bool IsActive { get; set; }
}
