using Application.Exceptions;
using Application.Services;
using Entities;
using Entities.Enums;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderService : GenericService<Order, OrderReadDTO, OrderService>, IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IOrderValidation _orderValidation;

    // private readonly IBaseRepository<OrderStatus> _orderStatusRepository;

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

    public async Task<PaginatedResult<OrderInfoDTO>> GetOrdersForItsStatusAsync(
        int productId,
        int orderStatusId,
        int pageNumber = 1,
        int pageSize = 50
    )
    {
        var orders = await _repository.GetOrdersForItsStatus(
            productId,
            orderStatusId,
            pageNumber,
            pageSize
        );

        var mappedData = orders
            .Data.Select(order => new OrderInfoDTO
            {
                OrderId = order.OrderId,
                CustomerName =
                    order.Customer != null
                        ? $"{order.Customer.FirstName} {order.Customer.LastName}"
                        : "Unknown",
                GovernorateName =
                    order.Customer != null && order.Customer.Addresses != null
                        ? order
                            .Customer.Addresses.FirstOrDefault(a => a.IsActive)
                            ?.Governorate?.Name ?? "Unknown"
                        : "Unknown",
                StreetAddress = order.Address != null ? order.Address.StreetAddress : "No Address",
                ProductId = order.ProductId,
                OrderStatusId = order.OrderStatusId,
                OrderReceiptStatusId = order.OrderReceiptStatusId,
                TotalPrice =
                    order.ProductQuantityPrice != null
                        ? order.ProductQuantityPrice.Quantity * order.ProductQuantityPrice.UnitPrice
                        : 0,
                Quantity = order.ProductQuantityPrice?.Quantity ?? 0,
                GovernorateDeliveryCost = order.GovernorateDelivery?.DeliveryCost,
                GovernorateDeliveryCompanyCost = order.GovernorateDeliveryCompany?.DeliveryCost,
                CreatedAt = order.CreatedAt,
                GovernorateId = order.Address.GovernorateId,
                DeliveryCompanyId = order.DeliveryCompanyId,
                Note = order.Note,
                CodeFromDeliveryCompany = order.CodeFromDeliveryCompany,
            })
            .ToList();

        return new PaginatedResult<OrderInfoDTO>
        {
            Data = mappedData,
            TotalCount = orders.TotalCount,
            TotalPages = orders.TotalPages,
            PageSize = orders.PageSize,
            CurrentPage = orders.CurrentPage,
            HasNext = orders.HasNext,
            HasPrevious = orders.HasPrevious,
        };
    }

 

    public async Task<OrderStatus_OrderReceipt> UpdateOrderStatus(
        OrderStatus_OrderReceipt orderStatus_OrderReceipt
    )
    {
        try
        {
            await _orderValidation.ValidateUpdateOrderStatus(
                orderStatus_OrderReceipt.OrderStatusId ?? 0
            );
            var order = await _repository.RetrieveTrackedAsync(o =>
                o.OrderId == orderStatus_OrderReceipt.OrderId
            );
            Utility.DoesExist(order);

            var enStatus = (enOrderStatus)orderStatus_OrderReceipt.OrderStatusId;

            var isStatusExist = Order_RECEIPT_STATUS_Mapper.OrderStatusToReceiptMap.ContainsKey(
                enStatus
            );

            if (isStatusExist)
                order.OrderStatusId = orderStatus_OrderReceipt.OrderStatusId ?? 1;
            else
                throw new Exception("OrderStatusId Doesn't Exist");

            if (Order_RECEIPT_STATUS_Mapper.OrderStatusToReceiptMap[enStatus] == null)
            {
                order.OrderReceiptStatusId = null;
            }
            else
            {
                if (orderStatus_OrderReceipt.OrderReceiptId == null)
                {
                    enOrderReceiptStatus? receiptId =
                        Order_RECEIPT_STATUS_Mapper.OrderStatusToReceiptMap[enStatus] != null
                            ? Order_RECEIPT_STATUS_Mapper.OrderStatusToReceiptMap[enStatus][0]
                            : null;
                    order.OrderReceiptStatusId = receiptId == null ? null : (int)receiptId;
                }
            }

            order.UpdatedAt = DateTime.UtcNow;

            var updatedOrder = _repository.Update(order);
            await this.SaveAsync();
            var dto = _mapper.Map<OrderReadDTO>(order);
            return new OrderStatus_OrderReceipt
            {
                OrderId = orderStatus_OrderReceipt.OrderId,
                OrderStatusId = updatedOrder.OrderStatusId,
                OrderReceiptId = updatedOrder.OrderReceiptStatusId,
            };
            ;
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
            order.UpdatedAt = DateTime.UtcNow;

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

    public async Task<GetCustomerOrdersInfo> GetCustomerOrdersInfo(int orderId)
    {
        try
        {
            var OrderCustomerInfo = await _repository
                .GetCustomerOrdersInfo(orderId)
                .Select(CustomerInfo => new GetCustomerOrdersInfo
                {
                    CustomerId = CustomerInfo.CustomerId,
                    FirstName = CustomerInfo.Customer.FirstName,
                    LastName = CustomerInfo.Customer.LastName,
                    PhoneNumber = CustomerInfo.Customer.PhoneNumber,
                    StreetAddress = CustomerInfo.Address.StreetAddress,
                    GovernorateName = CustomerInfo.Address.Governorate.Name,
                })
                .FirstOrDefaultAsync();

            return OrderCustomerInfo;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while getting customer info, {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<Order> UpdateOrderDeliveryCompany(
        int orderId,
        int deliveryCompanyId,
        int governorateId
    )
    {
        try
        {
            var order = await this._repository.RetrieveTrackedAsync(order =>
                order.OrderId == orderId
            );

            await _orderValidation.ValidateUpdateOrderDeliveryCompany(
                order,
                deliveryCompanyId,
                governorateId
            );

            order.DeliveryCompanyId = deliveryCompanyId;
            order.UpdatedAt = DateTime.UtcNow;

            var updatedOrder = this._repository.Update(order);

            return updatedOrder;
        }
        catch (Exception ex)
        {
            _logger.LogError("Cannot assign delivery company to the order", ex.Message);
            throw;
        }
    }

    public async Task<Order> UpdateOrderGovernorateDeliveryCompany(
        int orderId,
        int governorateDeliveryCompanyId
    )
    {
        try
        {
            var order = await this._repository.RetrieveTrackedAsync(order =>
                order.OrderId == orderId
            );
            Utility.DoesExist(order, "Order");
            order.GovernorateDeliveryCompanyId = governorateDeliveryCompanyId;
            order.UpdatedAt = DateTime.UtcNow;

            _repository.Update(order);

            return order;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to assign GovernorateDeliveryCompany to the order.");
            throw;
        }
    }

    public async Task<OrderReadDTO> UpdateOrderNote(int orderId, string note)
    {
        try
        {
            var order = await this._repository.RetrieveTrackedAsync(order =>
                order.OrderId == orderId
            );

            this._orderValidation.ValidateUpdateOrderNote(order, note);
            order.Note = note;
            order.UpdatedAt = DateTime.UtcNow;
            order = _repository.Update(order);

            await this.SaveAsync();

            return _mapper.Map<OrderReadDTO>(order);
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to assign note to the order.");
            throw;
        }
    }

    public async Task<OrderReadDTO> UpdateCodeForDeliveryCompany(
        int orderId,
        string deliveryCompanyCode
    )
    {
        try
        {
            var order = await this._repository.RetrieveTrackedAsync(order =>
                order.OrderId == orderId
            );

            await this._orderValidation.UpdateCodeForDeliveryCompany(
                order,
                orderId,
                deliveryCompanyCode
            );

            order.CodeFromDeliveryCompany = deliveryCompanyCode;

            order.UpdatedAt = DateTime.UtcNow;

            order = _repository.Update(order);

            await this.SaveAsync();

            var dto = new OrderReadDTO
            {
                OrderId = order.OrderId,
                CodeFromDeliveryCompany = order.CodeFromDeliveryCompany,
                UpdatedAt = order.UpdatedAt,
            };
            return dto;
        }
        catch (Exception ex)
        {
            _logger.LogError("Failed to assign CodeFromDeliveryCode to the order.");
            throw;
        }
    }
};
