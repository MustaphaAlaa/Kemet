using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Price
{
    [Key]
    public int PriceId { get; set; }

    [Required]
    public decimal MininmumPrice { get; set; }

    [Required]
    public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; } // Optional

    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}
