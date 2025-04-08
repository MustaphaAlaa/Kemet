
using Application.ColorServices;
using IServices.IColorServices;
using Microsoft.Extensions.DependencyInjection;

namespace Kemet.Application.Extensions;


public static partial class ApplicationLayerExtensions
{


    private static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<ICreateColor, CreateColorService>();
        service.AddScoped<IDeleteColor, DeleteColorService>();
        service.AddScoped<IUpdateColor, UpdateColorService>();
        service.AddScoped<IRetrieveColor, RetrieveColorService>();
        service.AddScoped<IRetrieveAllColors, RetrieveAllColorsService>();

    }
}


