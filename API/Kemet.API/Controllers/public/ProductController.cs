using System.Net;
using Entities;
using IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/product")]
[ApiController]
[AllowAnonymous]
public class ProductController : ControllerBase
{
    private ILogger<ProductController> _logger;
    IProductService _productService;
    APIResponse _response;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        this._productService = productService;
    }

    [HttpGet("")]
    public async Task<IActionResult> AllProducts()
    {
        _response = new();

        try
        {
            _logger.LogInformation($"ProductController => AllProducts()");

            var products = await _productService.RetrieveAllAsync();
            _response.Result = products;
            _response.IsSuccess = true;
            _response.StatusCode = products.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
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

    [HttpGet("{Id}")]
    public async Task<IActionResult> Product(int Id)
    {
        _response = new();

        try
        {
            _logger.LogInformation($"ProductController => Products({Id})");
            var product = await _productService.RetrieveByAsync(p => p.ProductId == Id);
            _response.Result = product;
            _response.IsSuccess = true;
            _response.StatusCode = product is not null
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
