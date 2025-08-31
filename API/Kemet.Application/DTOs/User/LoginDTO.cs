using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DTOs;

public class LoginDTO
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
}
