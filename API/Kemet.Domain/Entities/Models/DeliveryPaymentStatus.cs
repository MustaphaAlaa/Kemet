using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class DeliveryPaymentStatus
{
    [Key]
    public int DeliveryPaymentStatusId { get; set; }

    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}
