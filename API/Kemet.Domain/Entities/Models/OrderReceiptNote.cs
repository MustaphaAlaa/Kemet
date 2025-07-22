using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

/// <summary>
/// Notes for order Receiptment
/// </summary>
public class OrderReceiptNote
{
    [Key]
    public int OrderReceiptNotesId { get; set; }

    [Required]
    public string Note { get; set; }

    [Required]
    public DateTime CreateAt { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid CreatedBy { get; set; }
    public virtual User User { get; set; }
}
