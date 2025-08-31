using System.ComponentModel.DataAnnotations;

namespace Entities.Models.DTOs;

public class UserWithToken
{
    [EmailAddress]
    public string Email { get; set; }
    public string UserName { get; set; }

    public string Token { get; set; }
}

public class LoginDTO
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }
}
