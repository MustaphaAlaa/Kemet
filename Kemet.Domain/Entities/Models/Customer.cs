using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [ForeignKey("User")]
    public int? UserId { get; set; }
    public virtual User User { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime CreatedAt { get; set; }
}
