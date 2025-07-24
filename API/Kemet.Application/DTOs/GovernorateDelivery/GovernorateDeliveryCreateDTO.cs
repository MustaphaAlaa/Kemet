using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DTOs;

public class GovernorateDeliveryCreateDTO
{
    public decimal DeliveryCost { get; set; }

    public int GovernorateId { get; set; }
    public bool IsActive { get; set; }
}
