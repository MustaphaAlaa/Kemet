using System.Net;
using Application.Services.Orchestrator;
using Entities;
using IServices;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/e/orders")]
[ApiController]
public class OrderEmployeeController : ControllerBase
{
    private readonly ILogger<OrderEmployeeController> _logger;
    private readonly IUpdateOrderOrchestratorService _updateOrderOrchestrator;
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private APIResponse _response;

    public OrderEmployeeController(
        ILogger<OrderEmployeeController> logger,
        IUpdateOrderOrchestratorService updateOrderOrchestrator,
        IOrderService orderService,
        IOrderItemService orderItemService
    )
    {
        _logger = logger;
        _updateOrderOrchestrator = updateOrderOrchestrator;
        _orderService = orderService;
        _orderItemService = orderItemService;
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

    [HttpGet("product/{productId}/status/{orderStatusId}")]
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

    [HttpGet("customer/{orderId}")]
    public async Task<IActionResult> GetCustomerOrdersInfo(int orderId)
    {
        try
        {
            _logger.LogInformation(
                $"OrderController => GetCustomerOrdersInfo(int orderId) called."
            );
            var orders = await _orderService.GetCustomerOrdersInfo(orderId);
            _response.Result = orders;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                $"An error occurred while retrieving customer info for order with Id {orderId} ."
            );
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add(ex.Message);
            return BadRequest(_response);
        }
    }

    [HttpGet("orderItems/{orderId}")]
    public async Task<IActionResult> GetOrderItems(int orderId)
    {
        try
        {
            _logger.LogInformation($"OrderController => GetOrderItems(int orderId) called.");
            var orderItems = await _orderItemService.GetOrderItemsForOrder(orderId);
            _response.Result = orderItems;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                $"An error occurred while retrieving order items for id {orderId} ."
            );
            _response.IsSuccess = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.ErrorMessages.Add(ex.Message);
            return BadRequest(_response);
        }
    }

    [HttpPut("Status/{orderId}/{orderStatusId}")]
    public async Task<IActionResult> UpdateOrderStatus(int orderId, int orderStatusId)
    {
        try
        {
            _logger.LogInformation("OrderController => UpdateOrderStatus() called.");
            var order = await _orderService.UpdateOrderStatus(orderId, orderStatusId);
            _response.Result = order;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
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

    [HttpPut("Receipt/{orderId}/{orderReceiptStatusId}")]
    public async Task<IActionResult> UpdateOrderReceiptStatus(int orderId, int orderReceiptStatusId)
    {
        try
        {
            _logger.LogInformation("OrderController => UpdateOrderReceiptStatus() called.");
            var order = await _orderService.UpdateOrderReceiptStatus(orderId, orderReceiptStatusId);
            _response.Result = order;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
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

    [HttpPut("DeliveryCompany/{orderId}/{deliveryCompanyId}/{governorateId}")]
    public async Task<IActionResult> UpdateOrderDeliveryCompany(
        int orderId,
        int deliveryCompanyId,
        int governorateId
    )
    {
        try
        {
            _logger.LogInformation("OrderController => UpdateDeliveryCompany() called.");
            var deliveryCompanyDetailsDTO =
                await _updateOrderOrchestrator.UpdateDeliveryCompanyForOrder(
                    orderId,
                    deliveryCompanyId,
                    governorateId
                );
            _response.Result = deliveryCompanyDetailsDTO;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
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

    [HttpPut("Note/{orderId}")]
    public async Task<IActionResult> UpdateOrderNote(int orderId, [FromBody] string note)
    {
        try
        {
            _logger.LogInformation("OrderController => UpdateOrderNote() called.");
            var order = await _orderService.UpdateOrderNote(orderId, note);
            _response.Result = order;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
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

    [HttpPut("DeliveryCompanyCode/{orderId}")]
    public async Task<IActionResult> UpdateDeliveryCompanyCode(
        int orderId,
        [FromBody] string codeFromDeliveryCompany
    )
    {
        try
        {
            _logger.LogInformation("OrderController => UpdateDeliveryCompanyCode() called.");
            var order = await _orderService.UpdateCodeForDeliveryCompany(
                orderId,
                codeFromDeliveryCompany
            );
            _response.Result = order;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
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
