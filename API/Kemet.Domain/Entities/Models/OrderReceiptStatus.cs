using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

/// <summary>
/// For tracking the order Receiptement and its statues
/// </summary>
public class OrderReceiptStatus
{
    [Key] public int OrderReceiptStatusId { get; set; }
    [Required] public string Name { get; set; }
}