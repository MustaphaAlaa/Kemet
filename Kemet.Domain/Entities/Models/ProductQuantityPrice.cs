using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class ProductQuantityPrice
{
    [Key] public int ProductQuntityPriceId { get; set; }
    [Required] public int Quantity { get; set; }
    [Required] public decimal Price { get; set; }
    public bool IsActive { get; set; }
    public string? Note { get; set; }
    [ForeignKey("Product")] public int ProductId { get; set; }
    public virtual Product Product { get; set; }
}
