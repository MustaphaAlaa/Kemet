using Entities.Models.ConfKemetMapperConfigPackages;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Models.Extensions;

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
