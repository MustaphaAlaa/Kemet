using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

/// <summary>
/// Order Status is contains the status of the order.
/// </summary>
public class OrderStatus
{
    [Key]
    public int OrderStatusId { get; set; }

    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
