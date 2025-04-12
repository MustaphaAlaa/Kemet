﻿using Application;
using Application.Services;
using IServices;
using Microsoft.Extensions.DependencyInjection;

namespace Kemet.Application.Extensions;

public static partial class ApplicationLayerExtensions
{
    private static void AddServices(this IServiceCollection service)
    {
        service.AddScoped<IColorService, ColorService>();
        service.AddScoped<IGovernorateService, GovernorateService>();
    }
}
