namespace Entities.Models.DTOs;

// Customer DTOs
public class CustomerCreateDTO
{
    public int? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}
