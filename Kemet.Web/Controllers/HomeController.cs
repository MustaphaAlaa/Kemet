using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kemet.Web.Models;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Facades;
using Kemet.Application.Facades;

namespace Kemet.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public IColorFacade ColorFacade { get; }

    public HomeController(ILogger<HomeController> logger, IColorFacade colorFacade)
    {
        _logger = logger;
        ColorFacade = colorFacade;
    }

    public async Task<IActionResult> Index()
    {
        var colors = await ColorFacade.RetrieveAll();
        colors = colors.ToList();
        return View(colors);

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
