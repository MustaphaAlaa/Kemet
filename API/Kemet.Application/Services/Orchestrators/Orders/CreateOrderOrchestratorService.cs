using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.DTOs.Orchestrates;
using IRepository.Generic;
using IServices;
using IServices.Orchestrator;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

public class OrderOrchestratorService : IOrderOrchestratorService
{
    /*

           *asking the customer is this the first time to order from us

        *if yes => create customer record in the database.
                  create new address record in the database.


        *if no => ask the user for his phone number,
                and then check if the customer exists in the database
                    if exists => get the customer record from the database.
                        show the customer his data (address for now) and ask him if he wants to update it or not
                        if the user wants to update the address => ask him for the new address and update it in the database
                        if the user doesn't want to update the address => continue to the next step
                if not exist => create a new record for the customer



        *create a new order record in the database.
        *for each product variant in the order create a new order item record in the database


        Notify the admin about the new order (later)
    */

    private readonly IProductVariantService _productVariantService;
    private readonly IGovernorateService _governorateService;

    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly ICustomerOnboardingOrchestrator _customerOnboardingOrchestrator;
    private readonly IProductQuantityPriceService _productQuantityPriceService;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderOrchestratorService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public OrderOrchestratorService(
        IProductVariantService productVariantService,
        IGovernorateService governorateService,
        IOrderService orderService,
        IOrderItemService orderItemService,
        ICustomerOnboardingOrchestrator customerOnboardingOrchestrator,
        IProductQuantityPriceService productQuantityPriceService,
        IMapper mapper,
        ILogger<OrderOrchestratorService> logger,
        IUnitOfWork unitOfWork
    )
    {
        _productVariantService = productVariantService;
        _governorateService = governorateService;
        _orderService = orderService;
        _orderItemService = orderItemService;
        _customerOnboardingOrchestrator = customerOnboardingOrchestrator;
        _productQuantityPriceService = productQuantityPriceService;
        _mapper = mapper;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    private async Task CreateOrderItemsAsync(
        int orderId,
        CreatingOrderForAnonymousCustomerRequest request,
        ProductQuantityPriceReadDTO productQuantityPrice
    )
    {
        _logger.LogInformation(
            $"OrderOrchestratorService => CreatingOrderItemsAsync {orderId} with product quantity price {productQuantityPrice.ProductQuantityPriceId}."
        );

        foreach (var kvp in request.ProductVariantIdsWithQuantity)
        {
            var productVariantId = kvp.Key;
            var quantity = kvp.Value;
            var unitPrice = productQuantityPrice.UnitPrice;
            var totalPrice = productQuantityPrice.UnitPrice * quantity;

            var orderItem = new OrderItemCreateDTO
            {
                OrderId = orderId,
                ProductVariantId = productVariantId,
                Quantity = quantity,
                UnitPrice = unitPrice,
                TotalPrice = totalPrice,
            };

            await _orderItemService.CreateAsync(orderItem);

            // orderItems.Add(orderItem);
        }

        // await _orderItemService.CreateRangeAsync(orderItems);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task CreateOrder(CreatingOrderForAnonymousCustomerRequest request)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var productQuantityPrice = await _productQuantityPriceService.GetById(
                request.ProductQuantityPriceId
            );

            ValidateProductQuantityPrice(request, productQuantityPrice);

            var customerInfo = await _customerOnboardingOrchestrator.EnsureCustomerOnboardingAsync(
                request
            );

            var newOrder = new OrderCreateDTO
            {
                CustomerId = customerInfo.CustomerId,
                AddressId = customerInfo.AddressId,
                OrderTotalPrice = productQuantityPrice.UnitPrice * productQuantityPrice.Quantity,
                ProductQuantityPriceId = request.ProductQuantityPriceId,
                ProductId = productQuantityPrice.ProductId,
            };

            var order = await _orderService.CreateWithTrackingAsync(newOrder); // needed to be tracked

            await _unitOfWork.SaveChangesAsync();

            await CreateOrderItemsAsync(order.OrderId, request, productQuantityPrice);

            await _unitOfWork.CommitAsync();
        }
        catch (NotAvailableException ex)
        {
            _logger.LogWarning(ex, "Resource not available: {Message}", ex.Message);
            await _unitOfWork.RollbackAsync();
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                $"An error occurred while creating the order: {ex.Message}",
                ex.Message
            );
            await _unitOfWork.RollbackAsync();
            throw;
        }
    }

    private void ValidateProductQuantityPrice(
        CreatingOrderForAnonymousCustomerRequest request,
        ProductQuantityPriceReadDTO productQuantityPrice
    )
    {
        if (productQuantityPrice == null)
            throw new NotAvailableException(
                $"Product quantity price with id {request.ProductQuantityPriceId} is not available."
            );

        var requestProductVariantsQuantity = request.ProductVariantIdsWithQuantity.Sum(pv =>
            pv.Value
        );

        if (productQuantityPrice.Quantity != requestProductVariantsQuantity)
        {
            throw new Exception(
                "Items Quantity Doesn't Equals the ProductQuantityPrice's Quantity"
            );
        }
    }

    private async Task ProductVariantAvailabilityCheck(
        CreatingOrderForAnonymousCustomerRequest request
    )
    {
        bool isProductVariantAvailable;

        //to be clear
        int productVariantId,
            productVariantQuantity;

        foreach (var PQ in request.ProductVariantIdsWithQuantity)
        {
            //for readability only
            productVariantId = PQ.Key;
            productVariantQuantity = PQ.Value;

            isProductVariantAvailable =
                await _productVariantService.CheckProductVariantAvailability(
                    productVariantId,
                    productVariantQuantity
                );

            if (!isProductVariantAvailable)
                throw new NotAvailableException(
                    $"Product variant with id {request.ProductVariantIdsWithQuantity} is not available in the stock."
                );
        }
    }

    private async Task ValidateTheOrderRequest(CreatingOrderForAnonymousCustomerRequest request)
    {
        if (request is null)
            throw new Exception("The request is null.");

        if (request.ProductVariantIdsWithQuantity is null)
            throw new NotAvailableException("Product variant with quantity is not available.");

        if (request.ProductVariantIdsWithQuantity.Count == 0)
            throw new NotAvailableException("Product variant with quantity is not available.");

        if (request.GovernorateId == 0)
            throw new NotAvailableException("Governorate id is not available.");

        bool isGovernorateAvailable = await _governorateService.CheckGovernorateAvailability(
            request.GovernorateId
        );

        if (!isGovernorateAvailable)
            throw new NotAvailableException(
                $"Governorate with id {request.GovernorateId} is not available to Delivery."
            );

        await ProductVariantAvailabilityCheck(request);
    }
}

public class CreatingOrderDTO
{
    // Product-Variant Data
    public Dictionary<int, int> ProductVariantWithQuantity { get; set; }

    public short Quantity { get; set; }

    // Governorate
    public int GovernorateId { get; set; }

    //Address
    public bool IsAddressStillSame { get; set; }

    public string? StreetAddress { get; set; }

    // Anonymous Customer Data
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }

    public Guid? CustomerId { get; set; }
}
