using System.Net;
using Entities;
using Entities.Models.DTOs;
using Entities.Models.DTOs.Orchestrates;
using IServices;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/e/order")]
[ApiController]
public class OrderEmployeeController : ControllerBase
{
    private readonly ILogger<OrderEmployeeController> _logger;
    private readonly IOrderOrchestratorService _orderOrchestrator;
    private readonly IOrderService _orderService;
    private APIResponse _response;

    public OrderEmployeeController(
        ILogger<OrderEmployeeController> logger,
        IOrderOrchestratorService orderOrchestrator,
        IOrderService orderService
    )
    {
        _logger = logger;
        _orderOrchestrator = orderOrchestrator;
        _orderService = orderService;
        _response = new APIResponse();
    }

    [HttpGet("statuses")]
    public async Task<IActionResult> GetOrderStatuses()
    {
        try
        {
            _logger.LogInformation("OrderController => GetOrderStatuses() called.");
            var orderStatuses = await _orderService.GetOrderStatusesAsync();
            _response.Result = orderStatuses;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving order statuses.");
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add(ex.Message);
            return BadRequest(_response);
        }
    }

    [HttpGet("orders/product/{productId}/statuses/{orderStatusId}")]
    public async Task<IActionResult> GetOrderForStatuses(
        int productId,
        int orderStatusId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 2
    )
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
