using System.Linq.Expressions;
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Entities.Enums;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderService : GenericService<Order, OrderReadDTO>, IOrderService
{
    private readonly IBaseRepository<Order> _repository;
    private readonly IOrderValidation _orderValidation;

    public OrderService(
        IOrderValidation orderValidation,
        ServiceFacade_DependenceInjection<Order> facade
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

            order.CreatedAt = DateTime.Now;
            order.OrderStatusId = (int)OrderStatusEnum.Pending;
            order.OrderReceiptStatusId = null;
            order.IsPaid = null;

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

    public async Task<OrderReadDTO> Update(OrderUpdateDTO updateRequest)
    {
        try
        {
            await _orderValidation.ValidateUpdate(updateRequest);

            var order = _mapper.Map<Order>(updateRequest);

            order.UpdatedAt = DateTime.Now;

            var updatedOrder = _repository.Update(order);

            return _mapper.Map<OrderReadDTO>(order);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Order doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the order. {ex.Message}";
            _logger.LogInformation(msg);
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
