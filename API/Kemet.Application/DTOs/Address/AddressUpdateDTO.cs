namespace Entities.Models.DTOs;

public class AddressUpdateDTO
{
    public int AddressId { get; set; }
    public string StreetAddress { get; set; }
    public int GovernorateId { get; set; }
    public int CustomerId { get; set; }
}
