namespace Entities.Models.DTOs;

public class DeliveryCompanyDetailsDTO
{
    public int DeliveryCompanyId { get; set; }
    public int GovernorateDeliveryCompanyId { get; set; }
    public decimal GovernorateDeliveryCompanyCost { get; set; }
    public int OrderId { get; set; }
}
