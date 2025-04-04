namespace Entities.Models.DTOs;

public class AddressUpdateDTO
{
    public int AddressId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public int GovernorateId { get; set; }
    public int CustomerId { get; set; }
}
