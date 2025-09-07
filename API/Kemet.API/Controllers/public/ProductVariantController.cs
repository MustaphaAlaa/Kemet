using System.Net;
using Entities;
using IServices;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/productVariant")]
[ApiController]
public class ProductVariantController : ControllerBase
{
    private ILogger<ProductVariantController> _logger;
    IProductVariantService _productVariantService;
    IProductVariantDetailsService _productVariantDetailsService;
    APIResponse? _response;

    public ProductVariantController(
        ILogger<ProductVariantController> logger,
        IProductVariantService productVariantService,
        IProductVariantDetailsService productVariantDetailsService
    )
    {
        _productVariantDetailsService = productVariantDetailsService;
        _logger = logger;
        this._productVariantService = productVariantService;
    }

    /// <summary>
    /// Retrieve the Product's Size for a specific color and product id
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="colorId"></param>
    /// <returns><see cref="Task<IActionResult>"/></returns>
    [HttpGet("details/{productId}/{colorId}")]
    public async Task<IActionResult> RetrieveProductVarientColorsSizes(int productId, int colorId)
    {
        _response = new();

        try
        {
            _logger.LogInformation(
                $"ProductVariantController => RetrieveProductVarientColorsSizes({productId}, {colorId})"
            );
            var productVariants =
                await _productVariantDetailsService.RetrieveProductVarientColorsSizes(
                    productId,
                    colorId
                );
            _response.Result = productVariants;
            _response.IsSuccess = true;
            _response.StatusCode = productVariants is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound;
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

    /// <summary>
    /// Retrieve the Product's colors
    /// </summary>
    /// <param name="productId"></param>
    /// <returns><see cref="Task<IActionResult>"/></returns>
    ///
    [HttpGet("details/{productId}")]
    public async Task<IActionResult> RetrieveProductVarientColors(int productId)
    {
        _response = new();

        try
        {
            _logger.LogInformation(
                $"ProductVariantController => ProductVariantWithDetails({productId} )"
            );
            var productVariants = await _productVariantDetailsService.RetrieveProductVarientColors(
                productId
            );
            _response.Result = productVariants;
            _response.IsSuccess = true;
            _response.StatusCode = productVariants is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound;
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

    [HttpGet("details/{productId}/{colorId}/{sizeId}")]
    public async Task<IActionResult> RetrieveProductVarientStock(
        int productId,
        int colorId,
        int sizeId
    )
    {
        _response = new();

        try
        {
            _logger.LogInformation(
                $"ProductVariantController => ProductVariantWithDetails({productId},   {colorId},   {sizeId} )"
            );
            var productVariants = await _productVariantDetailsService.RetrieveProductVarientStock(
                productId,
                colorId,
                sizeId
            );
            _response.Result = productVariants;
            _response.IsSuccess = true;
            _response.StatusCode = productVariants is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.NotFound;
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
