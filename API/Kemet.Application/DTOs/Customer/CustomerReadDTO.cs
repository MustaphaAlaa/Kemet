namespace Entities.Models.DTOs;

public class CustomerReadDTO
{
    public Guid CustomerId { get; set; }
    // public Guid? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}
