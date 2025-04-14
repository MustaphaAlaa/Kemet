namespace Entities.Models.DTOs.GovernorateDelivery;

public class GovernorateDeliveryReadDTO
{
    public int DeliveryId { get; set; }

    public string CompanyName { get; set; }

    public decimal DeliveryCost { get; set; }

    public int GovernorateId { get; set; }
}
