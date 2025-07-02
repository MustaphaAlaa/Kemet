using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using IServices.Orchestrator;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

public class DeliveryCompanyOrchestratorService : IDeliveryCompanyOrchestratorService
{
    public DeliveryCompanyOrchestratorService(
        IDeliveryCompanyService deliveryCompanyService,
        IGovernorateDeliveryCompanyService governorateDeliveryCompanyService,
        IGovernorateService governorateService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<DeliveryCompanyOrchestratorService> logger
    )
    {
        _deliveryCompanyService = deliveryCompanyService;
        _governorateDeliveryCompanyService = governorateDeliveryCompanyService;
        _governorateService = governorateService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    private readonly IDeliveryCompanyService _deliveryCompanyService;
    private readonly IGovernorateDeliveryCompanyService _governorateDeliveryCompanyService;
    private readonly IGovernorateService _governorateService;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeliveryCompanyOrchestratorService> _logger;

    public async Task CreateDeliveryCompany(DeliveryCompanyCreateDTO createRequest)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var deliveryCompany = await _deliveryCompanyService.CreateWithTrackingAsync(
                createRequest
            );
            await _unitOfWork.SaveChangesAsync();

            var governorateList = await _governorateService.RetrieveAllAsync();

            var governorateDeliveryCompanyList = governorateList.Select(
                gdc => new GovernorateDeliveryCompanyCreateDTO()
                {
                    GovernorateId = gdc.GovernorateId,
                    DeliveryCompanyId = deliveryCompany.DeliveryCompanyId,
                    DeliveryCost = null,
                    IsActive = null,
                    CreatedAt = DateTime.UtcNow,
                }
            );

            await _governorateDeliveryCompanyService.AddRange(governorateDeliveryCompanyList);
            await _governorateDeliveryCompanyService.SaveAsync();
            await _unitOfWork.CommitAsync();
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            string errorMsg =
                "An unexpected error occurred while creating the delivery company and  governorate associated with it.";
            _logger.LogError(ex, errorMsg);
            throw;
        }
    }
}
