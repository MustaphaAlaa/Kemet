using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }

    [Required]
    [ForeignKey("Address")]
    public int AddressId { get; set; }
    public virtual Address Address { get; set; }
    public DateTime OrderDate { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}
