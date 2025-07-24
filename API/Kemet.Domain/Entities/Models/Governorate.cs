using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

/// <summary>
/// The Governorates
/// </summary>
public class Governorate
{
    [Key] public int GovernorateId { get; set; }

    [Required] public string Name { get; set; }

    [Required] public bool IsAvailableToDeliver { get; set; }


   public ICollection<GovernorateDeliveryCompany> GovernoratesDeliveryCompany { get; set; }
   public ICollection<GovernorateDelivery> GovernoratesDelivery { get; set; }
}