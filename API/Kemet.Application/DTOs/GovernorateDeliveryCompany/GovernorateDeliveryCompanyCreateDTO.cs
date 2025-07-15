namespace Entities.Models.DTOs;

public class GovernorateDeliveryCompanyCreateDTO
{
    public int DeliveryCompanyId { get; set; }
    public int GovernorateId { get; set; }
      /// <summary>
      /// DeliveryCost is nullable because when it automatically created it won't have value
      /// </summary>
    public decimal? DeliveryCost { get; set; }
    /// <summary>
    /// IsActive is nullable because when it automatically created it won't have value
    /// </summary>
    public bool? IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
}