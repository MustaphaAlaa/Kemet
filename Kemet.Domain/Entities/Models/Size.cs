using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Size
{
    [Key]
    public int SizeId { get; set; }
    public string Name { get; set; }
}
