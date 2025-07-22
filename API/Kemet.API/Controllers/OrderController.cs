using Entities.Models.DTOs.Orchestrates;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderOrchestratorService _orderOrchestrator;

    public OrderController(ILogger<OrderController> logger, IOrderOrchestratorService orderOrchestrator)
    {
        _logger = logger;
        _orderOrchestrator = orderOrchestrator;
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateOrder([FromBody] CreatingOrderForAnonymousCustomerRequest request)
    {
        try
        {
            _logger.LogInformation("OrderController => CreateOrder() called.");
            await _orderOrchestrator.CreateOrder(request);
            return Ok(new { Message = "Order created successfully." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the order.");
            return BadRequest(new { Error = ex.Message });
        }
    }


    [HttpGet("")]
    public IActionResult GetOrders()
    {
        // This method can be implemented to retrieve orders if needed.
        return Ok(new { Message = "GetOrders method is not implemented yet." });
    }
}
