using IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        public ColorController(ILogger<ColorController> logger)
        {
            _logger = logger;
        }

        private ILogger<ColorController> _logger;

        public async Task<IActionResult> Index()
        {
            return Ok();
        }
    }
}
