namespace Entities.Models.DTOs;

public class OrderCreateDTO
{
    public Guid CustomerId { get; set; }
    public int AddressId { get; set; }
}
