namespace Entities.Models.DTOs;

public class CustomerUpdateDTO
{
    public int CustomerId { get; set; }
    public int? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
}
