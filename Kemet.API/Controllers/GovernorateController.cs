using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Entities.API.Controllers;

[Route("api/Governorate")]
[ApiController]
public class GovernorateController : ControllerBase
{
    public GovernorateController(ILogger<GovernorateController> logger, IGovernorateService governorateService)
    {
        _logger = logger;
        this.governorateService = governorateService;
        _response = new();
    }
    readonly APIResponse _response;
    private ILogger<GovernorateController> _logger;
    IGovernorateService governorateService;

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {

        try
        {
            _logger.LogInformation($"GovernorateController => Index()");
            var colors = await governorateService.RetrieveAllAsync();
            _response.Result = colors;
            _response.IsSuccess = true;
            _response.StatusCode = colors.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
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
            _logger.LogInformation($"GovernorateController => GetGovernorate(GovernorateId {GovernorateId})");

            var color = await governorateService.RetrieveByAsync(color => color.GovernorateId == GovernorateId);
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