namespace Entities.Models.DTOs;

/// <summary>
/// GovernorateDeliveryReadDTO with additional properties needed for join the governorate entity
/// </summary>
public class GovernorateDeliveryDTO : GovernorateDeliveryReadDTO
{
    public string Name { get; set; }
}
