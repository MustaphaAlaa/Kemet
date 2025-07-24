using Entities;
using Entities.Models;
using Entities.Models.DTOs;
using IServices;
using Kemet.Application.Services.Orchestrators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
namespace Kemet.API.Controllers;



[Route("api/a/ProductQuantityPrice")]

[ApiController]

//[Authorize(Roles = "Admin")]
public class ProductQuantityPriceAdminController : ControllerBase
{
    private ILogger<ProductQuantityPriceAdminController> _logger;
    readonly IProductQuantityPriceService _productQuantityPriceService;
    readonly IProductPriceOrchestratorService _productPriceOrchestratorService;
    APIResponse _response;

    public ProductQuantityPriceAdminController(ILogger<ProductQuantityPriceAdminController> logger, IProductQuantityPriceService productQuantityPriceService, IProductPriceOrchestratorService productPriceOrchestratorService)
    {
        _logger = logger;
        _productQuantityPriceService = productQuantityPriceService;
        _productPriceOrchestratorService = productPriceOrchestratorService;
        _response = new();
    }

    [HttpPost("")]
    public async Task<ActionResult> CreataeProructQuantity(ProductQuantityPriceCreateDTO createRequest)
    {

        try
        {
            _logger.LogInformation($"ProductQuantityPriceAdminController => CreataeProructQuantity(ProductQuantityPriceCreateDTO: {createRequest})");

            //var productQuantityPrices = await _productQuantityPriceService.CreateAsync(createRequest);
            //await _productQuantityPriceService.SaveAsync();

            var productQuantityPrices = await this._productPriceOrchestratorService.CreateProductQuantityPrice(createRequest);
            _response.Result = productQuantityPrices;
            _response.IsSuccess = true;
            _response.StatusCode = productQuantityPrices is not null ? HttpStatusCode.OK : HttpStatusCode.NotFound;
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