using System.Net;
using Entities;
using Entities.Models.Interfaces.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/a/orders")]
[ApiController]
[Authorize(Roles = "Admin")]
public class OrderAdminController : ControllerBase
{
    private readonly ILogger<OrderAdminController> _logger;
    private readonly IExport _ex;
    private APIResponse _response;

    public OrderAdminController(ILogger<OrderAdminController> logger, IExport ex)
    {
        _logger = logger;
        _response = new APIResponse();
        _ex = ex;
    }

    [HttpPost("excel")]
    public async Task<IActionResult> Excel(int[] orders)
    {
        try
        {
            _logger.LogInformation("OrderController => Excel() called.");

            var file = await _ex.Export(new List<int>(orders));

            return File(
                file,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"Orders-{DateTime.UtcNow.ToString()}.xlsx"
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating order status.");
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add(ex.Message);
            return BadRequest(_response);
        }
    }
}
