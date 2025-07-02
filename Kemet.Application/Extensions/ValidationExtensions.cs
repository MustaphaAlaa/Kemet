using FluentValidation;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Models.Extensions;

public static partial class ApplicationLayerExtensions
{
    private static void AddValidations(this IServiceCollection service)
    {
        service.AddScoped<IColorValidation, ColorValidation>();

        service.AddScoped<IGovernorateValidation, GovernorateValidation>();

        service.AddScoped<IGovernorateDeliveryValidation, GovernorateDeliveryValidation>();

        service.AddScoped<ICustomerValidation, CustomerValidation>();

        service.AddScoped<ISizeValidation, SizeValidation>();
        service.AddScoped<IAddressValidation, AddressValidation>();

        service.AddScoped<IPriceValidation, PriceValidation>();

        service.AddScoped<IProductValidation, ProductValidation>();


        service.AddScoped<IProductVariantValidation, ProductVariantValidation>();
        service.AddScoped<IProductQuantityPriceValidation, ProductQuantityPriceValidation>();


        service.AddScoped<IOrderValidation, OrderValidation>();

        service.AddScoped<IOrderItemValidation, OrderItemValidation>();
        service.AddScoped<IDeliveryCompanyValidation, DeliveryCompanyValidation>();
        service.AddScoped<IGovernorateDeliveryCompanyValidation , GovernorateDeliveryCompanyValidation>();




        service.AddValidatorsFromAssemblyContaining<ColorCreateValidation>();
    }
}
