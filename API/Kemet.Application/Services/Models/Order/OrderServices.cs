using Application.Exceptions;
using Application.Services;
using Entities.Enums;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderService : GenericService<Order, OrderReadDTO, OrderService>, IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IOrderValidation _orderValidation;
    private readonly IBaseRepository<OrderStatus> _orderStatusRepository;

    public OrderService(
        IServiceFacade_DependenceInjection<Order, OrderService> facade,
        IOrderValidation orderValidation,
        IOrderRepository repository
    )
        : base(facade, "Order")
    {
        _repository = repository;
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

    public async Task<ICollection<OrderInfoDTO>> GetOrdersForItsStatusAsync(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    )
    {
        var orders = _repository.GetOrdersForItsStatus(
            productId,
            orderStatusId,
            pageNumber,
            pageSize
        );

        var orderInfoList = await orders
            .Select(o => new OrderInfoDTO
            {
                OrderId = o.OrderId,
                CustomerName = $"{o.Customer.FirstName} {o.Customer.LastName}",
                GovernorateName = o
                    .Customer.Addresses.FirstOrDefault(a => a.IsActive)
                    .Governorate.Name,
                StreetAddress = o.Address != null ? o.Address.StreetAddress : "No Address",
                ProductId = o.ProductId,
                OrderStatusId = o.OrderStatusId,
                OrderReceiptStatusId = o.OrderReceiptStatusId,
                TotalPrice = o.ProductQuantityPrice.Quantity * o.ProductQuantityPrice.UnitPrice,
                Quantity = o.ProductQuantityPrice.Quantity,
                CreatedAt = o.CreatedAt,
            })
            .ToListAsync();

        return orderInfoList;
    }

    public async Task<ICollection<OrderStatusReadDTO>> GetOrderStatusesAsync()
    {
        var orderStatuses = await _unitOfWork.GetRepository<OrderStatus>().RetrieveAllAsync();
        var lst = orderStatuses
            .Select(os => new OrderStatusReadDTO
            {
                OrderStatusId = os.OrderStatusId,
                Name = os.Name,
            })
            .ToList();
        return lst;
    }

    public async Task<OrderReadDTO> UpdateOrderStatus(int orderId, int orderStatusId)
    {
        try
        {
            await _orderValidation.ValidateUpdateOrderStatus(orderStatusId);
            var order = await _repository.RetrieveTrackedAsync(o => o.OrderId == orderId);
            Utility.DoesExist(order);
            order.OrderStatusId = orderStatusId;

            _repository.Update(order);
            await this.SaveAsync();
            var dto = _mapper.Map<OrderReadDTO>(order);
            return dto;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while update order status{ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<OrderReadDTO> UpdateOrderReceiptStatus(
        int orderId,
        int orderReceiptStatusId,
        string note = ""
    )
    {
        try
        {
            await _orderValidation.ValidateUpdateOrderReceiptStatus(orderReceiptStatusId);
            var order = await _repository.RetrieveTrackedAsync(o => o.OrderId == orderId);
            Utility.DoesExist(order);
            order.OrderReceiptStatusId = orderReceiptStatusId;

            _repository.Update(order);
            await this.SaveAsync();
            var dto = _mapper.Map<OrderReadDTO>(order);
            return dto;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while update order Receipt status{ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
};
