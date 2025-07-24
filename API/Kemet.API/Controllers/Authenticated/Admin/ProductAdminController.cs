using System.Net;
using Application.Services.Orchestrator;
using Entities;
using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/a/Product")]
[ApiController]
public class ProductAdminController : ControllerBase
{
    public ProductAdminController(
        ILogger<ProductAdminController> logger,
        IProductOrchestratorService productService
    )
    {
        _logger = logger;
        this._productService = productService;

    }

    APIResponse _response;
    private ILogger<ProductAdminController> _logger;
    readonly IProductOrchestratorService _productService;

    [HttpPost("")]
    public async Task<IActionResult> CreateProduct(
        [FromBody] ProductWithVariantsCreateDTO productWithItSpecificationCreateDTO
    )
    {
        _response = new();

        try
        {
            _logger.LogInformation(
                $"ProductAdminController => CreateProduct({productWithItSpecificationCreateDTO}) "
            );
            var newProduct = await _productService.AddProductWithSpecific(
                productWithItSpecificationCreateDTO
            );
            // await _productVariantService.SaveAsync();
            _response.IsSuccess = true;
            _response.StatusCode = newProduct
                ? HttpStatusCode.Created
                : HttpStatusCode.ExpectationFailed;
            _response.Result = newProduct;

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

    // [HttpPut("")]
    // public async Task<IActionResult> UpdateProduct([FromBody] ProductUpdateDTO productUpdateDTO)
    // {
    //     try
    //     {
    //         _logger.LogInformation($"ProductAdminController => UpdateColor({productUpdateDTO}) ");

    //         var newColor = await _productVariantService.Update(productUpdateDTO);
    //         await _productVariantService.SaveAsync();

    //         _response.IsSuccess = true;
    //         _response.StatusCode = newColor is not null
    //             ? HttpStatusCode.OK
    //             : HttpStatusCode.ExpectationFailed;

    //         return Ok(_response);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex.InnerException, ex.Message);

    //         _response.IsSuccess = false;
    //         _response.ErrorMessages = new() { ex.Message };
    //         _response.StatusCode = HttpStatusCode.ExpectationFailed;
    //         _response.Result = null;
    //         return BadRequest(_response);
    //     }
    // }

    // [HttpDelete("destroy/product/a/")]
    // public async Task<IActionResult> DeleteProduct([FromBody] ProductDeleteDTO productDeleteDTO)
    // {
    //     try
    //     {
    //         _logger.LogInformation($"ProductAdminController => DeleteProduct({productDeleteDTO}) ");

    //         await _productVariantService.DeleteAsync(productDeleteDTO);
    //         await _productVariantService.SaveAsync();

    //         var product = await _productVariantService.RetrieveByAsync(p =>
    //             p.ProductId == productDeleteDTO.ProductId
    //         );

    //         _response.IsSuccess = true;
    //         _response.StatusCode = product is not null
    //             ? HttpStatusCode.OK
    //             : HttpStatusCode.ExpectationFailed;

    //         return Ok(_response);
    //     }
    //     catch (Exception ex)
    //     {
    //         _logger.LogError(ex.InnerException, ex.Message);

    //         _response.IsSuccess = false;
    //         _response.ErrorMessages = new() { ex.Message };
    //         _response.StatusCode = HttpStatusCode.ExpectationFailed;
    //         _response.Result = null;
    //         return BadRequest(_response);
    //     }
    // }
}
