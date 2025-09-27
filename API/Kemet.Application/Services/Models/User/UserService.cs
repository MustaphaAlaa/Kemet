using Entities;
using Entities.Models;
using Entities.Models.DTOs;
using IServices;
using Kemet.Application.Interfaces.Services.Tokens;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<UserService> _logger;
    private readonly ITokenService _tokenService;

    public UserService(
        UserManager<User> userManager,
        ILogger<UserService> logger,
        SignInManager<User> signInManager,
        ITokenService tokenService
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _tokenService = tokenService;
    }

    public async Task<UserWithToken> Login(LoginDTO login)
    {
        try
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(user =>
                user.Email == login.Email
            );

            if (user == null)
                throw new Exception("Invalid Email");

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded)
                throw new Exception("Invalid Email Or Password");

            return new UserWithToken
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user),
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    async Task<UserWithToken> Signup(RegisterDTO register, string role)
    {
        try
        {
            if (register is null)
                throw new Exception("Who are fucking you are");

            if (register.Password != register.ConfirmPassword)
                throw new InvalidDataException("The Password Doesn't Match Confirm Password");

            var user = new User
            {
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
                UserName = register.UserName,
                FirstName = register.FirstName,
                SecondName = register.SecondName,
            };

            var createUser = await _userManager.CreateAsync(user, register.ConfirmPassword);
            if (createUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, role);
                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        "The Employee Role Cannot be added to that user"
                    );
                }
            }
            else
            {
                var errs = createUser.Errors.Select(e => e.Description);
                throw new InvalidOperationException(String.Join(", ", errs));
            }

            return new UserWithToken
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateTokenAsync(user),
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }

    public async Task<UserWithToken> AddEmployee(RegisterDTO register)
    {
        return await this.Signup(register, Roles.Employee);
    }
}
