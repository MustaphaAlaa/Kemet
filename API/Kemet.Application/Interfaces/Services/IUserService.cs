using Entities.Models.DTOs;

namespace IServices;

public interface IUserService
{
    Task<UserWithToken> Login(LoginDTO loginDTO);
    Task<UserWithToken> Signup(RegisterDTO register);
}
