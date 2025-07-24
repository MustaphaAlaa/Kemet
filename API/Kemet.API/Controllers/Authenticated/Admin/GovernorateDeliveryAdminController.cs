using System.Net;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entities.API.Controllers;

[Route("api/a/GovernorateDelivery")]
[ApiController]
// [Authorize(Roles = "admin")]
public class GovernorateDeliveryAdminController : ControllerBase
{
    public GovernorateDeliveryAdminController(
        ILogger<GovernorateDeliveryAdminController> logger,
        IGovernorateDeliveryService governorateDeliveryService
    )
    {
        _logger = logger;
        this._governorateDeliveryService = governorateDeliveryService;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<GovernorateDeliveryAdminController> _logger;
    IGovernorateDeliveryService _governorateDeliveryService;

    [HttpGet("all")]
    public async Task<IActionResult> allGovernorates()
    {
        try
        {
            _logger.LogInformation($"GovernorateDeliveryController => Index()");
            var governorateDeliveryLst =
                await _governorateDeliveryService.NullableActiveGovernoratesDelivery();
            _response.Result = governorateDeliveryLst;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
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

    [HttpGet("{GovernorateId}")]
    public async Task<IActionResult> GetGovernorate(int GovernorateId)
    {
        try
        {
            _logger.LogInformation(
                $"GovernorateDeliveryController => GetGovernorate(GovernorateId {GovernorateId})"
            );

            var color = await _governorateDeliveryService.RetrieveByAsync(color =>
                color.GovernorateId == GovernorateId
            );
            _response.Result = color;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
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

    [HttpPut("")]
    public async Task<IActionResult> UpdateGovernorateDeliveryCost(GovernorateDeliveryUpdateDTO gdc)
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => UpdateGovernorateDeliveryCompanyCost({gdc}) "
            );

            var governorateDeliveryCompany = await _governorateDeliveryService.DeactivateAndCreate(
                gdc
            );

            _response.Result = governorateDeliveryCompany;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

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
