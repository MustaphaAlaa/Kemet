using System.Net;
using Entities;
using Entities.Models.DTOs;
using IServices;
using IServices.Orchestrator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kemet.API.Controllers;

[Route("api/a/DeliveryCompany")]
[ApiController]
[Authorize(Roles = "Admin")]
public class DeliveryCompanyAdminController : ControllerBase
{
    public DeliveryCompanyAdminController(
        ILogger<DeliveryCompanyAdminController> logger,
        IDeliveryCompanyService deliveryCompanyService,
        IDeliveryCompanyOrchestratorService deliveryCompanyOrchestratorService,
        IGovernorateDeliveryCompanyService governorateDeliveryCompany
    )
    {
        _logger = logger;
        this._deliveryCompanyService = deliveryCompanyService;
        _deliveryCompanyOrchestratorService = deliveryCompanyOrchestratorService;
        _governorateDeliveryCompanyService = governorateDeliveryCompany;
        _response = new();
    }

    readonly APIResponse _response;
    private ILogger<DeliveryCompanyAdminController> _logger;
    IDeliveryCompanyService _deliveryCompanyService;
    IDeliveryCompanyOrchestratorService _deliveryCompanyOrchestratorService;

    IGovernorateDeliveryCompanyService _governorateDeliveryCompanyService;

    [HttpGet("all")]
    public async Task<IActionResult> GetAllDeliveryCompanies()
    {
        try
        {
            _logger.LogInformation($"DeliveryCompanyAdminController => GetAllDeliveryCompanies() ");
            // get all companies for available governorateId onlyF

            var deliveryCompanyReadDtos = await _deliveryCompanyService.RetrieveAllAsync();
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

            _response.Result = deliveryCompanyReadDtos;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpGet("{deliveryCompanyId}")]
    public async Task<IActionResult> GetDeliveryCompany(int deliveryCompanyId)
    {
        try
        {
            _logger.LogInformation($"DeliveryCompanyAdminController => GetAllDeliveryCompanies() ");
            // get all companies for available governorateId onlyF

            var deliveryCompanyReadDto = await _deliveryCompanyService.RetrieveByAsync(
                deliveryCompany => deliveryCompany.DeliveryCompanyId == deliveryCompanyId
            );
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

            _response.Result = deliveryCompanyReadDto;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpGet("activeGovernorate/{GovernorateId}")]
    public async Task<IActionResult> GetAllDeliveryCompaniesForGovernorate(int GovernorateId)
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => GetAllDeliveryCompaniesForGovernorate({GovernorateId}) "
            );

            var deliveryCompanyReadDtos =
                await _deliveryCompanyService.DeliveryCompanyForActiveGovernorate(GovernorateId);

            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

            _response.Result = deliveryCompanyReadDtos;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateDeliveryCompany(
        [FromBody] DeliveryCompanyCreateDTO createRequest
    )
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => CreateDeliveryCompany({createRequest}) "
            );
            await _deliveryCompanyOrchestratorService.CreateDeliveryCompany(createRequest);
            // await _deliveryCompanyService.SaveAsync();
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.Created;
            _response.Result = null;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpPut("")]
    public async Task<IActionResult> UpdateDeliveryCompany(
        [FromBody] DeliveryCompanyUpdateDTO updateRequest
    )
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => UpdateDeliveryCompany({updateRequest}) "
            );

            var newDeliveryCompany = await _deliveryCompanyService.Update(updateRequest);
            await _deliveryCompanyService.SaveAsync();

            _response.IsSuccess = true;
            _response.StatusCode = newDeliveryCompany is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.ExpectationFailed;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpDelete("")]
    public async Task<IActionResult> DeleteDeliveryCompany(
        [FromBody] DeliveryCompanyDeleteDTO deleteRequest
    )
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => DeleteDeliveryCompany({deleteRequest}) "
            );

            await _deliveryCompanyService.DeleteAsync(deleteRequest);
            await _deliveryCompanyService.SaveAsync();

            var color = await _deliveryCompanyService.RetrieveByAsync(c =>
                c.DeliveryCompanyId == deleteRequest.DeliveryCompanyId
            );

            _response.IsSuccess = true;
            _response.StatusCode = color is not null
                ? HttpStatusCode.OK
                : HttpStatusCode.ExpectationFailed;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpGet("{deliveryCompanyID}/activeGovernorates")]
    public async Task<IActionResult> ActiveGovernorateDeliveryCompany(int deliveryCompanyID)
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => ActiveGovernorateDeliveryCompany({deliveryCompanyID}) "
            );

            var lst = await _deliveryCompanyService.ActiveGovernorates(deliveryCompanyID);
            _response.Result = lst;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }

    [HttpPut("{deliveryCompanyID}/Governorate")]
    public async Task<IActionResult> UpdateGovernorateDeliveryCompanyCost(
        GovernorateDeliveryCompanyUpdateDTO gdc
    )
    {
        try
        {
            _logger.LogInformation(
                $"DeliveryCompanyAdminController => UpdateGovernorateDeliveryCompanyCost({gdc}) "
            );

            var governorateDeliveryCompany = await _governorateDeliveryCompanyService.SoftUpdate(
                gdc
            );
            _response.Result = governorateDeliveryCompany;
            _response.IsSuccess = true;
            _response.StatusCode = HttpStatusCode.OK;

            return Ok(_response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            _response.IsSuccess = false;
            _response.ErrorMessages = new() { ex.Message };
            _response.StatusCode = HttpStatusCode.ExpectationFailed;
            _response.Result = null;
            return BadRequest(_response);
        }
    }
}
