using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models;
// for now, I want models in one file because it easier for me to read 'em all 

public class Order
{
    [Key] public int OrderId { get; set; }
    [Required] [ForeignKey("Customer")] public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public DateTime OrderDate { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}

public class OrderItem
{
    [Key] public int OrderItemId { get; set; }
    
    [ForeignKey("Order")] public int OrderId { get; set; }
    public virtual Order Order { get; set; }
    
    [ForeignKey("ProductVariant")] public int ProductVariantId { get; set; }
    public virtual ProductVariant ProductVariant { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; } 
    public decimal TotalPrice { get; set; } 
    
}

public class Return
{
    [Key] public int ReturnId { get; set; }
    [ForeignKey("OrderItem")] public int OrderItemId { get; set; }
    public virtual OrderItem OrderItem { get; set; }

    [ForeignKey("User")] public int ReturnedBy { get; set; }
    public virtual User User { get; set; }

    public int Quantity { get; set; }

    public DateTime ReturnDate { get; set; }
    public string Notes { get; set; }
}

public class Product
{
    [Key] public int ProductId { get; set; }
    [Required] public string Name { get; set; }
    [Required] public string Description { get; set; }
    [Required] public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("Category")] public int CategoryId { get; set; }
    public virtual Category Category { get; set; }
}

public class ProductVariant
{
    [Key] public int ProductVariantId { get; set; }

    [ForeignKey("Product")] public int ProductId { get; set; }
    public virtual Product Product { get; set; }

    [ForeignKey("Color")] public int ColorId { get; set; }
    public virtual Color Color { get; set; }

    [ForeignKey("Size")] public int SizeId { get; set; }
    public virtual Size Size { get; set; }

    [Required] public int Stock { get; set; }
}

public class Size
{
    [Key] public int SizeId { get; set; }
    public string Name { get; set; }
}

public class Color
{
    public string ColorId { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
}

public class Category
{
    [Key] public int CategoryId { get; set; }
    [Required] public string NameAr { get; set; }
    [Required] public string NameEn { get; set; }
}

public class User : IdentityUser<int>
{
    [Key] public int UserId { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string SecondName { get; set; }
}

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

public class Governorate
{
    public int GovernorateId { get; set; }
    public string NameAr { get; set; }
    public string? NameEn { get; set; }
}

public class Customer
{
    [Key] public int CustomerId { get; set; }

    [ForeignKey("User")] public int? UserId { get; set; }
    public virtual User User { get; set; }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}