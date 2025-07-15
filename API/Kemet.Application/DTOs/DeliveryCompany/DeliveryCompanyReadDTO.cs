namespace Entities.Models.DTOs;

public class DeliveryCompanyReadDTO
{
    public int DeliveryCompanyId { get; set; }

    public string Name { get; set; }

    public DateTime DialingWithItFrom { get; set; }

    public bool IsActive { get; set; }

    public ICollection<Order>? Orders { get; set; }
    public ICollection<GovernorateDeliveryCompany>? GovernorateDeliveryCompanies { get; set; }
}
