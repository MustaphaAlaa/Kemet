using Application;
using Application.Services;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Utilities;
using IServices;
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Kemet.Application.Services.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Models.Extensions;

public static partial class ApplicationLayerExtensions
{
    private static void AddServices(this IServiceCollection service)
    {
        service.AddScoped(typeof(IServiceFacade_DependenceInjection<,>), typeof(ServiceFacade_DependenceInjectionq<,>));
        service.AddScoped(typeof(IRepositoryRetrieverHelper<>), typeof(RepositoryRetrieverHelper<>));

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
        //service.AddScoped<IProductQuantityPriceService, IProductQuantityPriceService>();

        service.AddScoped<IPriceService, PriceService>();
        service.AddScoped<IGovernorateDeliveryService, GovernorateDeliveryService>();


    }
}
