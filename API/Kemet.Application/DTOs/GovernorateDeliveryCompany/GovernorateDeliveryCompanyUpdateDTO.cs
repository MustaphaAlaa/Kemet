namespace Entities.Models.DTOs;

public class GovernorateDeliveryCompanyUpdateDTO
{
    public int GovernorateDeliveryCompanyId { get; set; }

    public decimal DeliveryCost { get; set; }

    public bool IsActive { get; set; }
}
