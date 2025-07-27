using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DTOs;

public class OrderStatusReadDTO
{
    [Key]
    public int OrderStatusId { get; set; }

    [Required]
    public string Name { get; set; }
}
