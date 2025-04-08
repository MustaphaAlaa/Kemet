using IServices.IColorServices;
using Kemet.Application.ConfKemetMapperConfiggPackages;
using Kemet.Application.Facades;
using Kemet.Application.Interfaces.Facades;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Validations;
using Microsoft.Extensions.DependencyInjection;
using Application.ColorServices;

namespace Kemet.Application.Extensions;

public static partial class ApplicationLayerExtensions
{
    public static void AddApplicationLayer(this IServiceCollection service)
    {
        AddServices(service);
        AddPackages(service);
        AddFacades(service);
        AddValidations(service);
    }

    private static void AddPackages(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(KemetMapperConfig));
    }

    private static void AddFacades(this IServiceCollection service)
    {
        service.AddScoped<IColorFacade, ColorFacade>();
        service.AddScoped<ISizeFacade, SizeFacade>();
    }
}

