using Entities.Models.DTOs;
using Kemet.Application.DTOs;

namespace IServices;

public interface IUserService
{
    Task<UserWithToken> Login(LoginDTO loginDTO);
    Task<UserWithToken> AddEmployee(RegisterDTO register);
    Task<IEnumerable<UserDto>> GetAllEmployees();
}
