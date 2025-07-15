using System.Net;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entities.API.Controllers;

[Route("api/a/Color")]
[ApiController]
public class ColorAdminController : ControllerBase
{
    public ColorAdminController(ILogger<ColorAdminController> logger, IColorService colorService)
    {
        _logger = logger;
        this._colorService = colorService;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<ColorAdminController> _logger;
    IColorService _colorService;

    [HttpPost("")]
    public async Task<IActionResult> CreateColor([FromBody] ColorCreateDTO colorCreateDTO)
    {
        try
        {
            _logger.LogInformation($"ColorAdminController => CreateColor({colorCreateDTO}) ");
            var newColor = await _colorService.CreateAsync(colorCreateDTO);
            await _colorService.SaveAsync();
            _response.IsSuccess = true;
            _response.StatusCode = newColor is not null
                ? System.Net.HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;
            _response.Result = newColor;

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
    public async Task<IActionResult> UpdateColor([FromBody] ColorUpdateDTO colorUpdateDTO)
    {
        try
        {
            _logger.LogInformation($"ColorAdminController => UpdateColor({colorUpdateDTO}) ");

            var newColor = await _colorService.Update(colorUpdateDTO);
            await _colorService.SaveAsync();

            _response.IsSuccess = true;
            _response.StatusCode = newColor is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.ExpectationFailed;

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

    [HttpDelete("")]
    public async Task<IActionResult> DeleteColor([FromBody] ColorDeleteDTO colorDeleteDTO)
    {
        try
        {
            _logger.LogInformation($"ColorAdminController => DeleteColor({colorDeleteDTO}) ");

            await _colorService.DeleteAsync(colorDeleteDTO);
            await _colorService.SaveAsync();

            var color = await _colorService.RetrieveByAsync(c =>
                c.ColorId == colorDeleteDTO.ColorId
            );

            _response.IsSuccess = true;
            _response.StatusCode = color is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.ExpectationFailed;

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
