using Entities;
using Entities.API.Controllers;
using Entities.Models;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;



[Route("api/Customer")]
[ApiController]
public class CustomerController : ControllerBase
{
    private ILogger<CustomerController> _logger;
    ICustomerService _customerService;
    APIResponse _response;
    public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
    {
        _logger = logger;
        this._customerService = customerService;


    }

    [HttpGet("Exist/{phoneNumber}")]
    public async Task<IActionResult> DoesCustomerExist(string phoneNumber)
    {
        _response = new();
        try
        {
            var exist = await _customerService.IsCustomerExist(phoneNumber);
            _response.IsSuccess = true;
            _response.ErrorMessages = null;
            _response.StatusCode = exist ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound;
            _response.Result = exist;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPost("Add")]
    public async Task<IActionResult> AddCustomer([FromBody] CustomerCreateDTO createDTO)
    {
        _response = new();

        try
        {
            var customer = await _customerService.CreateAsync(createDTO);
            _response.IsSuccess = true;
            _response.ErrorMessages = null;
            _response.StatusCode = customer is not null ? System.Net.HttpStatusCode.OK : System.Net.HttpStatusCode.NotFound;
            _response.Result = customer;
            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new();
            _response.ErrorMessages?.Add(ex.Message);
            _response.StatusCode = System.Net.HttpStatusCode.BadRequest;
            _response.Result = null;
            return BadRequest(_response);
        }
    }
}
