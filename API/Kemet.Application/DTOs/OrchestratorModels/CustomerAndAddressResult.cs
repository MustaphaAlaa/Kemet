namespace Entities.Models.DTOs.Orchestrates;

// A simple DTO to pass data to the next service
public class CustomerAndAddressResult
{
    public Guid CustomerId { get; set; }
    public int AddressId { get; set; } // Assuming Address ID is an int. Adjust if it's a Guid.
}

// record CustomerAndAddressResult(int AddressId, Guid customerId);
