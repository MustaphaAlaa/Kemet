using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Entities.Models;

public class User : IdentityUser<Guid>
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string SecondName { get; set; }
}
