using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DTOs;

public class OrderReceiptStatusReadDTO
{
    [Key]
    public int OrderReceiptStatusId { get; set; }

    [Required]
    public string Name { get; set; }
}
