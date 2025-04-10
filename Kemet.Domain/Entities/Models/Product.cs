using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Product
{
    [Key] public int ProductId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("Category")] public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<ProductQuantityPrice> QuantityPrices { get; set; }
    public virtual ICollection<Price> Prices { get; set; }
}


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



public class Price
{
    [Key] public int PriceId { get; set; }
    [Required] public decimal MininmumPrice { get; set; }
    [Required] public decimal MaximumPrice { get; set; }
    public DateTime? StartFrom { get; set; }
    public DateTime? EndsAt { get; set; }
    public string? Note { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? CreatedBy { get; set; } // Optional
    [ForeignKey("Product")] public int ProductId { get; set; }
    public virtual Product Product { get; set; }

}