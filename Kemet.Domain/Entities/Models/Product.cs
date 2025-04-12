using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductQuantityPrice> QuantityPrices { get; set; }
    public virtual ICollection<Price> Prices { get; set; }
}
