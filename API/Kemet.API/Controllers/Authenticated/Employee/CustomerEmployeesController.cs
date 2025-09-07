using System.Net;
using Entities;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

//e is for employees
[Route("api/e/Customer/")]
[ApiController]
[Authorize(Roles = "Admin, Employee")]
public class CustomerEmployeesController : ControllerBase
{
    private ILogger<CustomerEmployeesController> _logger;
    ICustomerService _customerService;
    readonly APIResponse _response;

    public CustomerEmployeesController(
        ILogger<CustomerEmployeesController> logger,
        ICustomerService customerService
    )
    {
        _logger = logger;
        this._customerService = customerService;

        _response = new();
    }

    [HttpGet("All")]
    public async Task<IActionResult> AllCustomers()
    {
        try
        {
            var allCustomers = await _customerService.RetrieveAllAsync();
            //var exist = await _productVariantService.IsCustomerExist(phoneNumber);
            _response.IsSuccess = true;
            _response.ErrorMessages = null;
            _response.StatusCode =
                allCustomers?.Count <= 0
                    ? System.Net.HttpStatusCode.OK
                    : System.Net.HttpStatusCode.NotFound;
            //_response.Result = exist;
            _response.Result = allCustomers;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("{phoneNumber}")]
    //authorize emp or admin
    public async Task<IActionResult> Customer(string phoneNumber)
    {
        try
        {
            var customer = await _customerService.FindCustomerByPhoneNumberAsync(phoneNumber);
            _response.IsSuccess = true;
            _response.StatusCode = customer is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound;
            _response.Result = customer;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;

            return BadRequest(_response);
        }
    }
}
