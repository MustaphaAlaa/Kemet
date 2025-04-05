using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Kemet.Web.Models;
using IServices.IColorServices;

namespace Kemet.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public IRetrieveAllColors Colors { get; }

    public HomeController(ILogger<HomeController> logger, IRetrieveAllColors colors)
    {
        _logger = logger;
        Colors = colors;
    }

    public async Task<IActionResult> Index()
    {
        var colors = await Colors.GetAllAsync();
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
