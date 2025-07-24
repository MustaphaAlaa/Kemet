namespace Entities.Models.DTOs.Orchestrates;

public class CreatingOrderForAnonymousCustomerRequest
{
    public int ProductQuantityPriceId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string? StreetAddress { get; set; }
    public bool SameLastAddress { get; set; }
    public int GovernorateId { get; set; }
    public Dictionary<int, int> ProductVariantIdsWithQuantity { get; set; }
}
