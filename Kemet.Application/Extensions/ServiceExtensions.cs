using Application;
using Application.Services;
using Application.Services.Orchestrator;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Entities.Models.Validations;
using IServices;
using IServices.Orchestrator;
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Kemet.Application.Services.Models;
using Kemet.Application.Services.Orchestrators;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Models.Extensions;

public static partial class ApplicationLayerExtensions
{
    private static void AddServices(this IServiceCollection service)
    {
        
        service.AddScoped(
            typeof(IServiceFacade_DependenceInjection<,>),
            typeof(ServiceFacade_DependenceInjection<,>)
        );
        service.AddScoped(
            typeof(IRepositoryRetrieverHelper<>),
            typeof(RepositoryRetrieverHelper<>)
        );

        //Services Models
        service.AddScoped<IColorService, ColorService>();
        service.AddScoped<IGovernorateService, GovernorateService>();

        service.AddScoped<ICustomerService, CustomerService>();
        service.AddScoped<IAddressService, AddressService>();

        service.AddScoped<IOrderService, OrderService>();
        service.AddScoped<IOrderItemService, OrderItemService>();

        service.AddScoped<ISizeService, SizeService>();

        service.AddScoped<IProductService, ProductService>();
        service.AddScoped<ICategoryService, CategoryService>();
        service.AddScoped<IProductVariantService, ProductVariantService>();
        service.AddScoped<IProductVariantDetailsService, ProductVariantDetailsService>();
        service.AddScoped<IProductOrchestratorService, ProductOrchestratorService>();
        service.AddScoped<IProductQuantityPriceService, ProductQuantityPriceService>();

        service.AddScoped<IPriceService, PriceService>();
        service.AddScoped<IGovernorateDeliveryService, GovernorateDeliveryService>();

        service.AddScoped<IProductPriceOrchestratorService, ProductPriceOrchestratorService>();
       
        service.AddScoped<IDeliveryCompanyOrchestratorService, DeliveryCompanyOrchestratorService>();
        
        service.AddScoped<IDeliveryCompanyService, DeliveryCompanyService>();
        service.AddScoped<IGovernorateDeliveryCompanyService, GovernorateDeliveryCompanyService>();

        
    }
}
