using Application.Exceptions;
using Entities.Models;
using IServices;

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
    private readonly IOrderService _orderService;
    private readonly IOrderItemService _orderItemService;
    private readonly IAddressService _addressService;

    public OrderOrchestratorService(
        IProductVariantService productVariantService,
        IGovernorateService governorateService,
        ICustomerService customerService,
        IOrderService orderService,
        IOrderItemService orderItemService,
        IAddressService addressService
    )
    {
        _productVariantService = productVariantService;
        _governorateService = governorateService;
        _customerService = customerService;
        _orderService = orderService;
        _orderItemService = orderItemService;
        _addressService = addressService;
    }

    public async Task CreateOrder(CreatingOrderDTO creatingOrderDTO)
    {
        try
        {
            await ValidateTheOrder(creatingOrderDTO);
        }
        catch (NotAvailableException ex) { }
        catch (Exception ex) { }
    }

    private async Task ValidateTheOrder(CreatingOrderDTO creatingOrderDTO)
    {
        bool isProductVariantAvailable =
            await _productVariantService.CheckProductVariantAvailability(
                creatingOrderDTO.ProductVariantId,
                creatingOrderDTO.Quantity
            );

        if (!isProductVariantAvailable)
            throw new NotAvailableException(
                $"Product variant with id {creatingOrderDTO.ProductVariantId} is not available in the stock."
            );

        bool isGovernorateAvailable = await _governorateService.CheckGovernorateAvailability(
            creatingOrderDTO.GovernorateId
        );

        if (!isGovernorateAvailable)
            throw new NotAvailableException(
                $"Governorate with id {creatingOrderDTO.ProductVariantId} is not available to Delivery."
            );
    }
}

public class CreatingOrderDTO
{
    // Product-Variant Data
    public int ProductVariantId { get; set; }

    public int Quantity { get; set; }

    // Governorate
    public int GovernorateId { get; set; }

    //Address
    public bool IsAddressStillSame { get; set; }

    public string? Address { get; set; }

    // Anonymous Customer Data
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }

    public int? CustomerId {get;set;}

}
