using Application.Exceptions;
using Application.Services;
using Entities.Enums;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderService : GenericService<Order, OrderReadDTO, OrderService>, IOrderService
{
    private readonly IBaseRepository<Order> _repository;
    private readonly IOrderValidation _orderValidation;

    public OrderService(
        IServiceFacade_DependenceInjection<Order, OrderService> facade,
        IOrderValidation orderValidation
    )
        : base(facade, "Order")
    {
        _repository = _unitOfWork.GetRepository<Order>();
        _orderValidation = orderValidation;
    }

    public async Task<OrderReadDTO> CreateAsync(OrderCreateDTO entity)
    {
        try
        {
            await _orderValidation.ValidateCreate(entity);

            var order = _mapper.Map<Order>(entity);

            order.CreatedAt = DateTime.UtcNow;
            order.OrderStatusId = (int)enOrderStatus.Pending;
            order.OrderReceiptStatusId = null;
            order.DeliveryCompanyId = null;
            
            //order.IsPaid = null;

            order = await _repository.CreateAsync(order);

            return _mapper.Map<OrderReadDTO>(order);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<Order> CreateWithTrackingAsync(OrderCreateDTO entity)
    {
        try
        {
            _logger.LogInformation(
                $"OrderService => CreateWithTrackingAsync {entity.ProductQuantityPriceId}."
            );
            await _orderValidation.ValidateCreate(entity);

            var order = _mapper.Map<Order>(entity);

            order.CreatedAt = DateTime.UtcNow;
            order.OrderStatusId = (int)enOrderStatus.Pending;
            order.OrderReceiptStatusId = null;
            //order.IsPaid = null;

            order = await _repository.CreateAsync(order);

            return order;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<OrderReadDTO> Update(OrderUpdateDTO updateRequest)
    {
        try
        {
            _logger.LogInformation($"OrderService => Update {updateRequest}.");
            await _orderValidation.ValidateUpdate(updateRequest);

            var order = _mapper.Map<Order>(updateRequest);

            order.UpdatedAt = DateTime.Now;

            var updatedOrder = _repository.Update(order);

            return _mapper.Map<OrderReadDTO>(order);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the order. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Order doesn't exist. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the order. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task DeleteAsync(OrderDeleteDTO entity)
    {
        try
        {
            await _orderValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.OrderId == entity.OrderId);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<OrderReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.OrderId == key);
    }
}
