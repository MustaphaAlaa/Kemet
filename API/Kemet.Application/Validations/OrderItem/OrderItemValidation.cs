using Application.Exceptions;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using IServices;

namespace Entities.Models.Validations;

public class OrderItemValidation : IOrderItemValidation
{
    private readonly IBaseRepository<OrderItem> _repository;
    private readonly IBaseRepository<Order> _orderRepository;
    private readonly IBaseRepository<ProductVariant> _productVariantRepository;

    private readonly IProductQuantityPriceService _productQuantityPriceService;

    private readonly IValidator<OrderItemCreateDTO> _OrderItemCreateValidation;
    private readonly IValidator<OrderItemUpdateDTO> _OrderItemUpdateValidation;
    private readonly IValidator<OrderItemDeleteDTO> _OrderItemDeleteValidation;

    public OrderItemValidation(
        IBaseRepository<OrderItem> repository,
        IBaseRepository<Order> orderRepository,
        IBaseRepository<ProductVariant> productVariantRepository,
        IProductQuantityPriceService productQuantityPriceService,
        IValidator<OrderItemCreateDTO> orderItemCreateValidation,
        IValidator<OrderItemUpdateDTO> orderItemUpdateValidation,
        IValidator<OrderItemDeleteDTO> orderItemDeleteValidation
    )
    {
        _repository = repository;
        _orderRepository = orderRepository;
        _productVariantRepository = productVariantRepository;
        _productQuantityPriceService = productQuantityPriceService;
        _OrderItemCreateValidation = orderItemCreateValidation;
        _OrderItemUpdateValidation = orderItemUpdateValidation;
        _OrderItemDeleteValidation = orderItemDeleteValidation;
    }

    public async Task ValidateCreate(OrderItemCreateDTO entity)
    {
        Utility.IsNull(entity);

        var validator = await _OrderItemCreateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        var productVariant = await _productVariantRepository.RetrieveAsync(p =>
            p.ProductVariantId == entity.ProductVariantId
        );

        Utility.DoesExist(productVariant, "Product Variant");

        if (productVariant.StockQuantity <= 0)
            throw new NotAvailableException(
                $"StockQuantity quantity is not available for Product Variant with id: {productVariant.ProductVariantId}"
            );

        var order = await _orderRepository.RetrieveAsync(p => p.OrderId == entity.OrderId);

        Utility.DoesExist(order, "Order");

        // var productQuantityPrice =
        //     await _productQuantityPriceService.ActiveProductPriceForQuantityWithId(
        //         productVariant.ProductId,
        //         entity.Quantity
        //     ); //
  
 
        // Utility.DoesExist(productQuantityPrice, "ProductQuantityPrice");

        // if (entity.UnitPrice != productQuantityPrice.UnitPrice)
        //     throw new InvalidPriceException(
        //         "Order-item UnitPrice didn't match the active price for this quantity"
        //     );
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

        var OrderItem = await _repository.RetrieveAsync(p => p.OrderItemId == entity.OrderItemId);

        Utility.DoesExist(OrderItem, "OrderItem");
    }
}
