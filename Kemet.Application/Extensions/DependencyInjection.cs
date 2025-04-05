using IServices.IColorServices;
using Kemet.Application.ConfKemetMapperConfiggPackages;
using Microsoft.Extensions.DependencyInjection;
using Services.ColorServices;

namespace Kemet.Application.Extensions;

public static class DependencyInjection
{
    public static void AddApplicationLayer(this IServiceCollection service)
    {
        AddServices(service);
        AddPackages(service);
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
}
