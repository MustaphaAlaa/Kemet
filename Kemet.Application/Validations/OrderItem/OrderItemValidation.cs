using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class OrderItemValidation : IOrderItemValidation
{
    private readonly IBaseRepository<OrderItem> _repository;
    private readonly IBaseRepository<Order> _orderRepository;
    private readonly IBaseRepository<ProductVariant> _productVariantRepository;

    private readonly IBaseRepository<Price> _priceRepository;

    private readonly IValidator<OrderItemCreateDTO> _OrderItemCreateValidation;
    private readonly IValidator<OrderItemUpdateDTO> _OrderItemUpdateValidation;
    private readonly IValidator<OrderItemDeleteDTO> _OrderItemDeleteValidation;

   

    public async Task ValidateCreate(OrderItemCreateDTO entity)
    {
        Utility.IsNull(entity);

        var validator = await _OrderItemCreateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        // var OrderItem = await _repository.RetrieveAsync(orderItem =>
        //     orderItem.CustomerId == entity.CustomerId
        //     && (entity.OrderItemDate - orderItem.OrderItemDate) <= TimeSpan.FromMinutes(1)
        // );

        // Utility.AlreadyExist(OrderItem, "OrderItem");
    }

    public async Task ValidateDelete(OrderItemDeleteDTO entity)
    {
        var validator = await _OrderItemDeleteValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
    }

    public async Task ValidateUpdate(OrderItemUpdateDTO entity)
    {
        var validator = await _OrderItemUpdateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        // entity.Name = entity.Name?.Trim().ToLower();

        var OrderItem = await _repository.RetrieveAsync(p => p.OrderItemId == entity.OrderItemId);

        Utility.DoesExist(OrderItem, "OrderItem");
    }
}
