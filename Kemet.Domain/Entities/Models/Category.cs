using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Category
{
    [Key] public int CategoryId { get; set; }
    [Required] public string NameAr { get; set; }
    [Required] public string NameEn { get; set; }
}
