using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Entities;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers
{
    [ApiController]
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userServices;
        private ILogger<AccountController> _logger;
        readonly APIResponse _response;

        public AccountController(IUserService userServices, ILogger<AccountController> logger)
        {
            _userServices = userServices;
            _logger = logger;
            _response = new APIResponse();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            try
            {
                _logger.LogInformation($"AccountAdminController => Login({loginDto})");
                var newUser = await _userServices.Login(loginDto);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;
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
