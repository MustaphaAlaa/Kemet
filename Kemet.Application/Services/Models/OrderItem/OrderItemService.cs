using Application.Exceptions;
using Application.Services;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderItemService : GenericService<OrderItem, OrderItemReadDTO>, IOrderItemService
{
    private readonly IBaseRepository<OrderItem> _repository;
    private readonly IOrderItemValidation _orderItemValidation;

    public OrderItemService(
        IOrderItemValidation orderItemValidation,
        ServiceFacade_DependenceInjection<OrderItem> facade
    )
        : base(facade, "OrderItem")
    {
        _repository = _unitOfWork.GetRepository<OrderItem>();
        _orderItemValidation = orderItemValidation;
    }

    public async Task<OrderItemReadDTO> CreateAsync(OrderItemCreateDTO entity)
    {
        try
        {
            await _orderItemValidation.ValidateCreate(entity);

            var orderItem = _mapper.Map<OrderItem>(entity);

            orderItem.TotalPrice = entity.Quantity * entity.UnitPrice;

            orderItem = await _repository.CreateAsync(orderItem);

            return _mapper.Map<OrderItemReadDTO>(orderItem);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<OrderItemReadDTO> Update(OrderItemUpdateDTO updateRequest)
    {
        try
        {
            await _orderItemValidation.ValidateUpdate(updateRequest);

            var orderItem = _mapper.Map<OrderItem>(updateRequest);

            var updatedOrderItem = _repository.Update(orderItem);

            return _mapper.Map<OrderItemReadDTO>(orderItem);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"{TName} doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task DeleteAsync(OrderItemDeleteDTO entity)
    {
        try
        {
            await _orderItemValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.OrderItemId == entity.OrderItemId);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<OrderItemReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.OrderItemId == key);
    }
}
