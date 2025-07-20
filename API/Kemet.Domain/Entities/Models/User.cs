using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User : IdentityUser<Guid>
{
    [Key]
    public Guid UserId { get; set; }

    [Required]
    public string FirstName { get; set; }

    [Required]
    public string SecondName { get; set; }
}
