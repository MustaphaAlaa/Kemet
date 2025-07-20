using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services.Orchestrator;

public class OrderOrchestratorService
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

    private readonly ICustomerService _customerService;
    private readonly IAddressService _addressService;

    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;

    private readonly IMapper _mapper;
    private readonly ILogger<OrderOrchestratorService> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public async Task CreateOrder(CreatingOrderDTO creatingOrderDTO)
    {
        try
        {
            await ValidateTheOrderRequest(creatingOrderDTO);

            var customerInfo = await DealingWithCustomerInfo(creatingOrderDTO);

            AddressReadDTO address = customerInfo.address;

            var customerId = customerInfo.customerId;

            var newOrder = new OrderCreateDTO
            {
                CustomerId = customerId,
                AddressId = address.AddressId,
            };

            var order = await _orderService.CreateAsync(newOrder);

            await _unitOfWork.SaveChangesAsync();
        }
        catch (NotAvailableException ex) { }
        catch (Exception ex) { }
    }

    private async Task<CustomerInfoRecord> DealingWithCustomerInfo(
        CreatingOrderDTO creatingOrderDTO
    )
    {
        AddressReadDTO address;

        Guid customerId;

        if (creatingOrderDTO.CustomerId is not null && creatingOrderDTO.StreetAddress is null)
        {
            address = await _addressService.GetActiveAddressByCustomerId(
                creatingOrderDTO.CustomerId.Value
            );
            customerId = creatingOrderDTO.CustomerId.Value;
        }
        else
        {
            var newCustomer = _mapper.Map<CustomerCreateDTO>(creatingOrderDTO);
            var customer = await _customerService.CreateAsync(newCustomer);
            customerId = customer.CustomerId;

            var newAddress = new AddressCreateDTO
            {
                CustomerId = customer.CustomerId,
                StreetAddress = creatingOrderDTO?.StreetAddress ?? "",
                GovernorateId = creatingOrderDTO?.GovernorateId ?? 0,
            };

            address = await _addressService.CreateAsync(newAddress);
        }

        return new CustomerInfoRecord(address, customerId);
    }

    private async Task ProductVariantAvailabilityCheck(CreatingOrderDTO creatingOrderDTO)
    {
        bool isProductVariantAvailable;

        //to be clear
        int productVariantId,
            productVariantQuantity;

        foreach (var PQ in creatingOrderDTO.ProductVariantWithQuantity)
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
                    $"Product variant with id {creatingOrderDTO.ProductVariantWithQuantity} is not available in the stock."
                );
        }
    }

    private async Task ValidateTheOrderRequest(CreatingOrderDTO creatingOrderDTO)
    {
        if (creatingOrderDTO.ProductVariantWithQuantity is null)
            throw new NotAvailableException("Product variant with quantity is not available.");

        if (creatingOrderDTO.ProductVariantWithQuantity.Count == 0)
            throw new NotAvailableException("Product variant with quantity is not available.");

        if (creatingOrderDTO.GovernorateId == 0)
            throw new NotAvailableException("Governorate id is not available.");

        bool isGovernorateAvailable = await _governorateService.CheckGovernorateAvailability(
            creatingOrderDTO.GovernorateId
        );

        if (!isGovernorateAvailable)
            throw new NotAvailableException(
                $"Governorate with id {creatingOrderDTO.GovernorateId} is not available to Delivery."
            );
    }

    private record CustomerInfoRecord(AddressReadDTO address, Guid customerId);
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
