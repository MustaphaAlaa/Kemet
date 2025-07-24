using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Address
{
    [Key]
    public int AddressId { get; set; }

    [Required]
    public string StreetAddress { get; set; }

    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey("Governorate")]
    public int GovernorateId { get; set; }
    public virtual Governorate Governorate { get; set; }

    [ForeignKey("Customer")]
    public Guid CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
