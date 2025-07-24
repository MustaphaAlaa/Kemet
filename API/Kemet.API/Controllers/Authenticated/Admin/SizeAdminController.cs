using System.Net;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace Entities.API.Controllers;

[Route("api/a/size")]
[ApiController]
public class SizeAdminController : ControllerBase
{
    public SizeAdminController(ILogger<SizeAdminController> logger, ISizeService sizeService)
    {
        _logger = logger;
        this._sizeService = sizeService;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<SizeAdminController> _logger;
    ISizeService _sizeService;

    [HttpPost("")]
    public async Task<IActionResult> CreateSize([FromBody] SizeCreateDTO sizeCreateDTO)
    {
        try
        {
            _logger.LogInformation($"SizeAdminController => CreateSize({sizeCreateDTO}) ");
            var newSize = await _sizeService.CreateAsync(sizeCreateDTO);
            await _sizeService.SaveAsync();
            _response.IsSuccess = true;
            _response.StatusCode = newSize is not null
                ? HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;
            _response.Result = newSize;

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
    public async Task<IActionResult> UpdateSize([FromBody] SizeUpdateDTO sizeUpdateDTO)
    {
        try
        {
            _logger.LogInformation($"SizeAdminController => UpdateSize({sizeUpdateDTO}) ");

            var newSize = await _sizeService.Update(sizeUpdateDTO);
            await _sizeService.SaveAsync();

            _response.IsSuccess = true;
            _response.StatusCode = newSize is not null
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
    public async Task<IActionResult> DeleteSize([FromBody] SizeDeleteDTO sizeDeleteDTO)
    {
        try
        {
            _logger.LogInformation($"SizeAdminController => DeleteSize({sizeDeleteDTO}) ");

            await _sizeService.DeleteAsync(sizeDeleteDTO);
            await _sizeService.SaveAsync();

            var size = await _sizeService.RetrieveByAsync(c => c.SizeId == sizeDeleteDTO.SizeId);

            _response.IsSuccess = true;
            _response.StatusCode = size is not null
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
