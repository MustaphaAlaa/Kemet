namespace Entities.Models.DTOs;

public class DeliveryCompanyUpdateDTO
{
    public int DeliveryCompanyId { get; set; }

    public string Name { get; set; }

    public bool IsActive { get; set; }

    public DateTime DialingWithItFrom { get; set; }
}
