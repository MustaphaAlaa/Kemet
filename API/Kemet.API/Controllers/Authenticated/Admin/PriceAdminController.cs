using System.Net;
using Entities.Models.DTOs;
using IServices;
using Kemet.Application.Services.Orchestrators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Entities.API.Controllers;

[Route("api/a/prices")]
[ApiController]
[Authorize(Roles = "Admin")]
public class PriceAdminController : ControllerBase
{
    public PriceAdminController(
        ILogger<PriceAdminController> logger,
        IProductPriceOrchestratorService productPriceOrchestratorService,
        IPriceService priceService
    )
    {
        _logger = logger;
        this._productPriceOrchestratorService = productPriceOrchestratorService;
        this._priceService = priceService;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<PriceAdminController> _logger;
    IProductPriceOrchestratorService _productPriceOrchestratorService;
    IPriceService _priceService;

    [HttpGet("product/range/{productId}")]
    public async Task<IActionResult> GetPrice(int productId)
    {
        try
        {
            // check if there's no price for the product
            _logger.LogInformation($"PriceAdminController => GetPrice({productId}) ");
            var price = await _priceService.ProductActivePrice(productId);

            _response.IsSuccess = true;

            _response.StatusCode = price is not null
                ? HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;

            _response.Result = price;

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

    [HttpPost("quantity/orc")]
    public async Task<IActionResult> CreatePriceOrchestra(
        [FromBody] ProductPriceOrchestratorCreateDTO createRequest
    )
    {
        try
        {
            // check if there's no price for the product
            _logger.LogInformation($"PriceAdminController => CreatePrice({createRequest}) ");
            var priceOrchestrator = await _productPriceOrchestratorService.CreateProductPrice(
                createRequest
            );

            _response.IsSuccess = true;

            _response.StatusCode = priceOrchestrator is not null
                ? HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;

            _response.Result = priceOrchestrator;

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

    [HttpPost("price/range")]
    public async Task<IActionResult> CreatePriceRange([FromBody] PriceCreateDTO createRequest)
    {
        try
        {
            _logger.LogInformation($"PriceAdminController => CreatePrice({createRequest}) ");
            var price = await _productPriceOrchestratorService.CreatePriceRange(createRequest);

            _response.IsSuccess = true;

            _response.StatusCode = price is not null
                ? HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;

            _response.Result = price;

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

    [HttpPut("price/range/note")]
    public async Task<IActionResult> UpdatePriceRangeNote([FromBody] PriceUpdateDTO updateRequest)
    {
        try
        {
            _logger.LogInformation($"PriceAdminController => CreatePrice({updateRequest}) ");
            var price = await _priceService.UpdateNote(updateRequest);
          await  _priceService.SaveAsync();
            _response.IsSuccess = true;

            _response.StatusCode = price is not null
                ? HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;

            _response.Result = price;

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
    public async Task<IActionResult> UpdatePrice([FromBody] PriceUpdateDTO priceUpdateDTO)
    {
        try
        {
            _logger.LogInformation($"PriceAdminController => UpdatePrice({priceUpdateDTO}) ");

            // var newPrice = await _productPriceOrchestratorService.Update(priceUpdateDTO);
            // await _productPriceOrchestratorService.SaveAsync();

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.ExpectationFailed;

            // _response.StatusCode = newPrice is not null
            //     ? HttpStatusCode.OK
            //     : HttpStatusCode.ExpectationFailed;

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
    public async Task<IActionResult> DeletePrice([FromBody] PriceDeleteDTO priceDeleteDTO)
    {
        try
        {
            _logger.LogInformation($"PriceAdminController => DeletePrice({priceDeleteDTO}) ");

            // await _productPriceOrchestratorService.DeleteAsync(priceDeleteDTO);
            // await _productPriceOrchestratorService.SaveAsync();

            // var price = await _productPriceOrchestratorService.RetrieveByAsync(c =>
            //     c.PriceId == priceDeleteDTO.PriceId
            // );

            _response.IsSuccess = true;
            // _response.StatusCode = price is not null
            //     ? HttpStatusCode.OK
            //     : HttpStatusCode.ExpectationFailed;

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
