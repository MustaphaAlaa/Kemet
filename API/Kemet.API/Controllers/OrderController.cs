using System.Net;
using Entities;
using Entities.Models.DTOs;
using Entities.Models.DTOs.Orchestrates;
using IServices;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/order")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderOrchestratorService _orderOrchestrator;
    private readonly IOrderService _orderService;
    private APIResponse _response;

    public OrderController(
        ILogger<OrderController> logger,
        IOrderOrchestratorService orderOrchestrator,
        IOrderService orderService
    )
    {
        _logger = logger;
        _orderOrchestrator = orderOrchestrator;
        _orderService = orderService;
        _response = new APIResponse();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreatingOrderForAnonymousCustomerRequest request
    )
    {
        try
        {
            _logger.LogInformation("OrderController => CreateOrder() called.");
            await _orderOrchestrator.CreateOrder(request);
            _response.Result = new { Message = "Order created successfully." };
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the order.");
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add(ex.Message);
            return BadRequest(_response);
        }
    }

    [HttpGet("")]
    public IActionResult GetOrders()
    {
        // This method can be implemented to retrieve orders if needed.
        return Ok(new { Message = "GetOrders method is not implemented yet." });
    }

    [HttpGet("statuses")]
    public async Task<IActionResult> GetOrderForStatuses(int productId, int orderStatusId, int pageNumber = 1, int pageSize = 2)
    {
        try
        {
            _logger.LogInformation("OrderController => GetOrderForStatuses() called.");
            var orders = await _orderService.GetOrdersForItsStatusAsync(
                 productId,
                 orderStatusId,
                 pageNumber,
                 pageSize
               
            );
            _response.Result = orders;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving orders for statuses.");
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add(ex.Message);
            return BadRequest(_response);
        }
    }
}
