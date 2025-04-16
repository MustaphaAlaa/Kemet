using Entities.Models.DTOs;
using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Entities.API.Controllers
{
    [Route("api/Color")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        public ColorController(ILogger<ColorController> logger, IColorService colorService)
        {
            _logger = logger;
            this.colorService = colorService;
        }

        private ILogger<ColorController> _logger;
        IColorService colorService;
        [HttpGet("index")]
        public async Task<IActionResult> Index()
        {

            try
            { //var co = await colorService.CreateAsync(null);
                var co = await colorService.CreateAsync(new ColorCreateDTO
                {
                    HexaCode = "",
                    Name = null
                });
                return Ok(co);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
