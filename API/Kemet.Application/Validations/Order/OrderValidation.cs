using Entities.Enums;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository;
using IRepository.Generic;

namespace Entities.Models.Validations;

public class OrderValidation : IOrderValidation
{
    private readonly IOrderRepository _repository;
    private readonly IBaseRepository<OrderReceiptStatus> _orderReceiptStatusRepository;
    private readonly IBaseRepository<OrderStatus> _orderStatusRepository;
    private readonly IDeliveryCompanyRepository _deliveryRepository;
    private readonly IValidator<OrderCreateDTO> _OrderCreateValidation;
    private readonly IValidator<OrderUpdateDTO> _OrderUpdateValidation;
    private readonly IValidator<OrderDeleteDTO> _OrderDeleteValidation;

    public OrderValidation(
        IOrderRepository repository,
        IBaseRepository<OrderReceiptStatus> orderReceiptStatusRepository,
        IBaseRepository<OrderStatus> orderStatusRepository,
        IValidator<OrderCreateDTO> orderCreateValidation,
        IValidator<OrderUpdateDTO> orderUpdateValidation,
        IValidator<OrderDeleteDTO> orderDeleteValidation,
        IDeliveryCompanyRepository deliveryRepository
    )
    {
        _repository = repository;
        _orderReceiptStatusRepository = orderReceiptStatusRepository;
        _orderStatusRepository = orderStatusRepository;
        _OrderCreateValidation = orderCreateValidation;
        _OrderUpdateValidation = orderUpdateValidation;
        _OrderDeleteValidation = orderDeleteValidation;
        _deliveryRepository = deliveryRepository;
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

        //if (order.IsPaid is not null && entity.IsPaid is null)
        //    entity.IsPaid = order.IsPaid;

        if (order.OrderReceiptStatusId is not null && entity.OrderReceiptStatusId is null)
            entity.OrderReceiptStatusId = order.OrderReceiptStatusId;
    }

    public async Task ValidateUpdateOrderReceiptStatus(int orderReceiptStatusId)
    {
        var ors = await _orderReceiptStatusRepository.RetrieveAsync(or =>
            or.OrderReceiptStatusId == orderReceiptStatusId
        );
        Utility.DoesExist(ors);
    }

    public async Task ValidateUpdateOrderStatus(int orderStatusId)
    {
        var os = await _orderStatusRepository.RetrieveAsync(os =>
            os.OrderStatusId == orderStatusId
        );
        Utility.DoesExist(os);
    }

    public async Task ValidateUpdateOrderDeliveryCompany(
        Order order,
        int deliveryCompanyId,
        int governorateId
    )
    {
        Utility.DoesExist(order, "Order");

        var deliveryCompany = this
            ._deliveryRepository.DeliveryCompanyForActiveGovernorate(governorateId)
            .FirstOrDefault(deliveryCompany =>
                deliveryCompany.DeliveryCompanyId == deliveryCompanyId
            );

        Utility.DoesExist(deliveryCompany, "Delivery Company");

        if (
            !(
                order?.OrderStatusId == (int)enOrderStatus.Pending
                || order?.OrderStatusId == (int)enOrderStatus.Processing
            )
        )
            throw new Exception("The Delivery Company Cannot be updated for this order.");
    }

    public void ValidateUpdateOrderNote(Order order, string note)
    {
        Utility.DoesExist(order, "Order");

        if (string.IsNullOrEmpty(note))
            throw new Exception("The note is null or empty");

        if (order.Note == note)
            throw new Exception("It's the same note saved.");
    }
}
