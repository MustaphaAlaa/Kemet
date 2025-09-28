using System.Net;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entities.API.Controllers;

[Route("api/GovernorateDelivery")]
[ApiController]
[AllowAnonymous]

public class GovernorateDeliveryController : ControllerBase
{
    public GovernorateDeliveryController(
        ILogger<GovernorateDeliveryController> logger,
        IGovernorateDeliveryService governorateDeliveryService
    )
    {
        _logger = logger;
        this.governorateDeliveryService = governorateDeliveryService;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<GovernorateDeliveryController> _logger;
    IGovernorateDeliveryService governorateDeliveryService;

    [HttpGet("all")]
    public async Task<IActionResult> allGovernorates()
    {
        try
        {
            _logger.LogInformation($"GovernorateDeliveryController => Index()");
            var governorateDeliveryLst =
                await governorateDeliveryService.ActiveGovernoratesDelivery();
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

            var color = await governorateDeliveryService.RetrieveByAsync(color =>
                color.GovernorateId == GovernorateId
            );
            _response.Result = color;
            _response.IsSuccess = true;
            _response.StatusCode = color is not null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
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
