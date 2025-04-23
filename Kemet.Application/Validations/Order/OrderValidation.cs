using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class OrderValidation : IOrderValidation
{
    private readonly IBaseRepository<Order> _repository;
    private readonly ILogger<OrderValidation> _logger;
    private readonly IValidator<OrderCreateDTO> _OrderCreateValidation;
    private readonly IValidator<OrderUpdateDTO> _OrderUpdateValidation;
    private readonly IValidator<OrderDeleteDTO> _OrderDeleteValidation;

    public OrderValidation(
        IBaseRepository<Order> repository,
        ILogger<OrderValidation> logger,
        IValidator<OrderCreateDTO> OrderCreateValidation,
        IValidator<OrderUpdateDTO> OrderUpdateValidation,
        IValidator<OrderDeleteDTO> OrderDeleteValidation
    )
    {
        _repository = repository;
        _logger = logger;
        _OrderCreateValidation = OrderCreateValidation;
        _OrderUpdateValidation = OrderUpdateValidation;
        _OrderDeleteValidation = OrderDeleteValidation;
    }

    public async Task ValidateCreate(OrderCreateDTO entity)
    {
        Utility.IsNull(entity);

        var validator = await _OrderCreateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
    }

    public async Task ValidateDelete(OrderDeleteDTO entity)
    {
        var validator = await _OrderDeleteValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
    }

    public async Task ValidateUpdate(OrderUpdateDTO entity)
    {
        var validator = await _OrderUpdateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        var order = await _repository.RetrieveAsync(p => p.OrderId == entity.OrderId);

        Utility.DoesExist(order, "Order");

        if (order.IsPaid is not null && entity.IsPaid is null)
            entity.IsPaid = order.IsPaid;

        if (order.OrderReceiptStatusId is not null && entity.OrderReceiptStatusId is null)
            entity.OrderReceiptStatusId = order.OrderReceiptStatusId;

    }
}
