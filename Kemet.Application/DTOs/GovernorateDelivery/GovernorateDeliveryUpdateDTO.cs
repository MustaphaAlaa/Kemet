namespace Entities.Models.DTOs.GovernorateDelivery;

public class GovernorateDeliveryUpdateDTO
{
    public int DeliveryId { get; set; }

    public string CompanyName { get; set; }

    public decimal DeliveryCost { get; set; }

    public int GovernorateId { get; set; }
}
