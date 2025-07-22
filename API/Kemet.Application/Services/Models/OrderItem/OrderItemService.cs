using Application.Exceptions;
using Application.Services;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderItemService
    : GenericService<OrderItem, OrderItemReadDTO, OrderItemService>,
        IOrderItemService
{
    private readonly IRangeRepository<OrderItem> _repository;
    private readonly IOrderItemValidation _orderItemValidation;

    public OrderItemService(
        IServiceFacade_DependenceInjection<OrderItem, OrderItemService> facade,
        IOrderItemValidation orderItemValidation,
        IRangeRepository<OrderItem> repository
    )
        : base(facade, "OrderItem")
    {
        _repository = repository;
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

    public async Task<OrderItem> CreateWithTrackingAsync(OrderItemCreateDTO entity)
    {
        try
        {
            _logger.LogInformation(
                $"OrderItemService => CreateWithTrackingAsync {entity.ProductVariantId}."
            );
            await _orderItemValidation.ValidateCreate(entity);

            var orderItem = _mapper.Map<OrderItem>(entity);

            orderItem.TotalPrice = entity.Quantity * entity.UnitPrice;

            orderItem = await _repository.CreateAsync(orderItem);

            return orderItem;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the {TName} inside CreateWithTrackingAsync. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the {TName}. {ex.Message}";
            _logger.LogError(msg);
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

    // public async Task<OrderItemReadDTO> AddRange(IEnumerable<OrderItemCreateDTO> entities)
    // {
    //     var orderItems = _mapper.Map<IEnumerable<OrderItem>>(entities);
    //     await _repository.AddRangeAsync(orderItems.ToArray());
    //     return _mapper.Map<IEnumerable<OrderItemReadDTO>>(orderItems);
    // }
}
