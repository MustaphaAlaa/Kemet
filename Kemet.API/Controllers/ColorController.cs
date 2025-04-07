using Kemet.Application.Facades;
using Kemet.Application.Interfaces.Facades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {

        public ColorController(ILogger<ColorController> logger, IColorFacade colorFacade)
        {
            _logger = logger;
            ColorFacade = colorFacade;
        }

        private ILogger<ColorController> _logger;

        public IColorFacade ColorFacade { get; }

        public async Task<IActionResult> Index()
        {
            var colors = await ColorFacade.RetrieveAll();
            colors = colors.ToList();
            return Ok(colors);

        }


    }
}
