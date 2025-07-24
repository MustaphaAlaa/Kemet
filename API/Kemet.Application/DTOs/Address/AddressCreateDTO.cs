namespace Entities.Models.DTOs;

public class AddressCreateDTO
{
    public string StreetAddress { get; set; }
    public int GovernorateId { get; set; }
    public Guid CustomerId { get; set; }
}
