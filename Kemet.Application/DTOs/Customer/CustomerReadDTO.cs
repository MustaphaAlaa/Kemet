namespace Entities.Models.DTOs;

public class CustomerReadDTO
{
    public int CustomerId { get; set; }
    public int? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}
