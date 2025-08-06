using System.Net;
using Entities;
using Entities.Models.DTOs;
using IServices;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/e/orders/helper")]
[ApiController]
public class OrderHelperEmployeeController : ControllerBase
{
    private readonly ILogger<OrderHelperEmployeeController> _logger;

    private readonly IOrderStatusService _orderStatusService;
    private readonly IOrderReceiptStatusService _orderReceiptStatusService;
    private APIResponse _response;

    public OrderHelperEmployeeController(
        ILogger<OrderHelperEmployeeController> logger,
        IOrderReceiptStatusService orderReceiptStatusService,
        IOrderStatusService orderStatusService
    )
    {
        _logger = logger;

        _orderStatusService = orderStatusService;
        _orderReceiptStatusService = orderReceiptStatusService;
        _response = new APIResponse();
    }

    [HttpGet("statuses")]
    public async Task<IActionResult> GetOrderStatuses()
    {
        try
        {
            _logger.LogInformation("OrderController => GetOrderStatuses() called.");
            var orderStatuses = await _orderStatusService.RetrieveAllAsync();
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

    [HttpGet("ReceiptStatuses")]
    public async Task<IActionResult> GetOrderReceiptStatuses()
    {
        try
        {
            _logger.LogInformation("OrderController => GetOrderReceiptStatuses() called.");
            var orderReceiptStatuses = await _orderReceiptStatusService.RetrieveAllAsync();
            _response.Result = orderReceiptStatuses;
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
}
