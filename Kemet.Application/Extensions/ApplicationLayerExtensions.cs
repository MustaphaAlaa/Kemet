using Kemet.Application.ConfKemetMapperConfigPackages;
using Microsoft.Extensions.DependencyInjection;

namespace Kemet.Application.Extensions;

public static partial class ApplicationLayerExtensions
{
    public static void AddApplicationLayer(this IServiceCollection service)
    {
        AddServices(service);
        AddPackages(service);
        AddValidations(service);
    }

    private static void AddPackages(this IServiceCollection service)
    {
        service.AddAutoMapper(typeof(KemetMapperConfig));
    }
}
