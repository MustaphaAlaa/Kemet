using System.Net;
using Entities;
using IServices;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers
{
    [Route("api/ProductQuantityPrice")]
    [ApiController]
    public class ProductQuantityPriceController : ControllerBase
    {
        private ILogger<ProductQuantityPriceController> _logger;
        readonly IProductQuantityPriceService _productQuantityPriceService;
        APIResponse _response;

        public ProductQuantityPriceController(
            ILogger<ProductQuantityPriceController> logger,
            IProductQuantityPriceService productQuantityPriceService
        )
        {
            _logger = logger;
            _productQuantityPriceService = productQuantityPriceService;

            _response = new();
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> ProductQuantityPricseFor(int ProductId)
        {
            try
            {
                _logger.LogInformation(
                    $"ProductQuantityPriceController => ProductQuantityPriceFor(ProductId: {ProductId})"
                );
                var productQuantityPrices =
                    await _productQuantityPriceService.ActiveQuantityPriceFor(ProductId);
                _response.Result = productQuantityPrices;
                _response.IsSuccess = true;
                _response.StatusCode = productQuantityPrices is not null
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
}
