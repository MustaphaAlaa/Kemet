using Entities;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Kemet.API.Controllers
{
    [Route("api/size")]
    [ApiController]
    public class SizeController : ControllerBase
    {
        public SizeController(ILogger<SizeController> logger, ISizeService sizeService)
        {
            _logger = logger;
            this._sizeService = sizeService;
            _response = new();
        }
        readonly APIResponse _response;
        private ILogger<SizeController> _logger;
        readonly ISizeService _sizeService;

        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {

            try
            {
                _logger.LogInformation($"public SizeController => Index()");
                var sizes = await _sizeService.RetrieveAllAsync();
                _response.Result = sizes;
                _response.IsSuccess = true;
                _response.StatusCode = sizes.Count > 0 ? HttpStatusCode.OK : HttpStatusCode.NotFound;
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
