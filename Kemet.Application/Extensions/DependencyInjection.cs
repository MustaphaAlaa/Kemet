using IServices.IColorServices;
using Kemet.Application.ConfKemetMapperConfiggPackages;
using Kemet.Application.Facades;
using Kemet.Application.Interfaces.Facades;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Validations;
using Microsoft.Extensions.DependencyInjection;
using Services.ColorServices;

namespace Kemet.Application.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection service)
    {
        AddServices(service);
        AddPackages(service);
        AddFacades(service);
        AddValidations(service);
    }

    private static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<ICreateColor, CreateColorService>();
        service.AddScoped<IDeleteColor, DeleteColorService>();
        service.AddScoped<IUpdateColor, UpdateColorService>();
        service.AddScoped<IRetrieveColor, RetrieveColorService>();
        service.AddScoped<IRetrieveAllColors, RetrieveAllColorsService>();
    }

    private static void AddPackages(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(KemetMapperConfig));
    }

    private static void AddValidations(this IServiceCollection service)
    {
        service.AddScoped<ICreateColorValidation, CreateColorValidation>();
        service.AddScoped<IUpdateColorValidation, UpdateColorValidation>();
    }
    private static void AddFacades(this IServiceCollection service)
    {
        service.AddScoped<IColorFacade, ColorFacade>();
        service.AddScoped<ISizeFacade, SizeFacade>();
    }
}
