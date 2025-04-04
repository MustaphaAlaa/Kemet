using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Address
{
    [Key] public int AddressId { get; set; }

    [Required] public string Street { get; set; }
    [Required] public string City { get; set; }

    [ForeignKey("Governorate")] public int GovernorateId { get; set; }
    public virtual Governorate Governorate { get; set; }

    [ForeignKey("Customer")] public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
}
