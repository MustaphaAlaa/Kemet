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
    private APIResponse _response;

    public OrderController(
        ILogger<OrderController> logger,
        IOrderOrchestratorService orderOrchestrator
    )
    {
        _logger = logger;
        _orderOrchestrator = orderOrchestrator;
        _response = new APIResponse();
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateOrder(
        [FromBody] CreatingOrderForAnonymousCustomerRequest request
    )
    {
        try
        {
            
            
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
}
