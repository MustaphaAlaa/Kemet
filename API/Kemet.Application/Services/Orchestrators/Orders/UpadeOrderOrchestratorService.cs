using AutoMapper;
using Entities.Models.DTOs;
using Entities.Models.Utilities;
using IRepository.Generic;
using IServices;
using IServices.Orchestrator;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

public class UpdateOrderOrchestratorService : IUpdateOrderOrchestratorService
{
    private readonly IGovernorateDeliveryCompanyService _governorateDeliveryCompanyService;

    private readonly IOrderService _orderService;
    private readonly IDeliveryCompanyService _deliveryCompanyService;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderOrchestratorService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderOrchestratorService(
        IOrderService orderService,
        IDeliveryCompanyService deliveryCompanyService,
        IGovernorateDeliveryCompanyService governorateDeliveryCompanyService,
        IMapper mapper,
        ILogger<UpdateOrderOrchestratorService> logger,
        IUnitOfWork unitOfWork
    )
    {
        this._governorateDeliveryCompanyService = governorateDeliveryCompanyService;
        _orderService = orderService;
        _deliveryCompanyService = deliveryCompanyService;
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<DeliveryCompanyDetailsDTO> UpdateDeliveryCompanyForOrder(
        int orderId,
        int deliveryCompanyId,
        int governorateId
    )
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var order = await _orderService.UpdateOrderDeliveryCompany(
                orderId,
                deliveryCompanyId,
                governorateId
            );

            await _unitOfWork.SaveChangesAsync();

            var governorateDeliveryCompany =
                await _governorateDeliveryCompanyService.GovernorateDeliveryCompanyAvailability(
                    deliveryCompanyId,
                    governorateId
                );

            Utility.DoesExist(governorateDeliveryCompany, "GovernorateDeliveryCompany");

            if (
                governorateDeliveryCompany.DeliveryCost is null
                || governorateDeliveryCompany.DeliveryCost <= 0
            )
                throw new Exception(
                    "Cannot assign GovernorateDeliveryCompany to the order. it's have null or zero cost."
                );

            await _orderService.UpdateOrderGovernorateDeliveryCompany(
                orderId,
                governorateDeliveryCompany.GovernorateDeliveryCompanyId
            );

            await _unitOfWork.SaveChangesAsync();

            await this._unitOfWork.CommitAsync();

            var deliveryCompanyDetailsDTO = new DeliveryCompanyDetailsDTO
            {
                OrderId = orderId,
                DeliveryCompanyId = deliveryCompanyId,
                GovernorateDeliveryCompanyCost = governorateDeliveryCompany.DeliveryCost ?? 0,
                GovernorateDeliveryCompanyId = governorateDeliveryCompany.GovernorateId,
            };

            return deliveryCompanyDetailsDTO;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackAsync();
            _logger.LogError(
                "Failed to assign GovernorateDeliveryCompany to the order",
                ex.Message
            );
            throw;
        }
    }
}
