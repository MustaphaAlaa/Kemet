using Entities;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kemet.API.Controllers.Authenticated.Admin;

//a is for admin
[Route("api/a/Customer/")]
[ApiController]
//[Authorize("admin")]
public class CustomerAdminController : ControllerBase
{
    private ILogger<CustomerAdminController> _logger;
    ICustomerService _customerService;
    readonly APIResponse _response;
    public CustomerAdminController(ILogger<CustomerAdminController> logger, ICustomerService customerService)
    {
        _logger = logger;
        _customerService = customerService;

        _response = new();
    }

    [HttpDelete("{phoneNumber}")]
    public async Task<IActionResult> DeleteCustomer(string phoneNumber)
    {

        Console.WriteLine("YOu're in delete");


        try
        {

            var c = new CustomerDeleteDTO { PhoneNumber = phoneNumber };
            await _customerService.DeleteAsync(c);
            await _customerService.SaveAsync();
            var customer = await _customerService.RetrieveByAsync(c => c.PhoneNumber == phoneNumber);
            _response.IsSuccess = true;
            _response.ErrorMessages = null;
            _response.StatusCode = customer is null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
            _response.Result = customer;
            return Ok(_response);
        }
        catch (Exception ex)
        {

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;

            return BadRequest();
        }
    }



}
