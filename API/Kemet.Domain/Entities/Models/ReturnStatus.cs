using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

// Keep your existing OrderItem and Order classes as they are

/// <summary>
/// For tracking return statuses throughout the return process
/// </summary>

public class ReturnStatus
{
    [Key]
    public int ReturnStatusId { get; set; }

    [Required]
    public string Name { get; set; }

    public string Description { get; set; }

    public ICollection<ReturnItem> ReturnItems { get; set; }
}
