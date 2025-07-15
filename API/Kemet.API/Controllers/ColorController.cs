using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Entities.API.Controllers;

[Route("api/Color")]
[ApiController]
public class ColorController : ControllerBase
{
    public ColorController(ILogger<ColorController> logger, IColorService colorService)
    {
        _logger = logger;
        this.colorService = colorService;
        _response = new();
    }
    readonly APIResponse _response;
    private ILogger<ColorController> _logger;
    IColorService colorService;

    [HttpGet("index")]
    public async Task<IActionResult> Index()
    {

        try
        {
            _logger.LogInformation($"ColorController => Index()");
            var colors = await colorService.RetrieveAllAsync();
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

    [HttpGet("{ColorId}")]
    public async Task<IActionResult> GetColor(int ColorId)
    {

        try
        {
            _logger.LogInformation($"ColorController => GetColor(ColorId {ColorId})");

            var color = await colorService.RetrieveByAsync(color => color.ColorId == ColorId);
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