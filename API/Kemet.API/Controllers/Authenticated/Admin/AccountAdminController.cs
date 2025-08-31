using System.Net;
using Entities;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers.Authenticated.Admin
{
    [ApiController]
    [Route("api/a/Account")]
    [AllowAnonymous]
    public class AccountAdminController : ControllerBase
    {
        private readonly IUserService _userServices;
        private ILogger<AccountAdminController> _logger;
        readonly APIResponse _response;

        public AccountAdminController(
            IUserService userServices,
            ILogger<AccountAdminController> logger
        )
        {
            _userServices = userServices;
            _logger = logger;
            _response = new APIResponse();
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] RegisterDTO registerDTO)
        {
            try
            {
                _logger.LogInformation($"AccountAdminController => Signup({registerDTO}) ");
                var newUser = await _userServices.Signup(registerDTO);
                _response.IsSuccess = true;
                _response.StatusCode = System.Net.HttpStatusCode.Created;
                _response.Result = newUser;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message);

                _response.IsSuccess = false;
                _response.ErrorMessages = new() { ex.Message };
                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.Result = null;
                return BadRequest(_response);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                _logger.LogInformation($"AccountAdminController => Login({loginDto}) ");
                var newUser = await _userServices.Login(loginDto);
                _response.IsSuccess = true;
                _response.StatusCode =  HttpStatusCode.Created;
                _response.Result = newUser;

                return Ok(_response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.InnerException, ex.Message);

                _response.IsSuccess = false;
                _response.ErrorMessages = new() { ex.Message };
                _response.StatusCode = HttpStatusCode.ExpectationFailed;
                _response.Result = null;
                return BadRequest(_response);
            }
        }
    }
}
