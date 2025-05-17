using Application;
using Application.Services;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Utilities;
using IServices;
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Entities.Models.Extensions;

public static partial class ApplicationLayerExtensions
{
    private static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<IColorService, ColorService>();
        service.AddScoped(typeof(IServiceFacade_DependenceInjection<,>), typeof(ServiceFacade_DependenceInjectionq<,>));
        service.AddScoped<IGovernorateService, GovernorateService>();
        service.AddScoped(typeof(IRepositoryRetrieverHelper<>), typeof(RepositoryRetrieverHelper<>));
    }
}
