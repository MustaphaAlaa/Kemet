namespace Kemet.Application.DTOs;

public class GovernorateDeliveryCompanyUpdateDTO
{
    public int GovernorateDeliveryCompanyId { get; set; }
    
    public int DeliveryCompanyId { get; set; }
    
    public int GovernorateId { get; set; }
  
    public decimal  DeliveryCost { get; set; }
  
    public bool  IsActive { get; set; }
}