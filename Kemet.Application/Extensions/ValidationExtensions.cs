using FluentValidation;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace Kemet.Application.Extensions;

public static partial class ApplicationLayerExtensions
{
    private static void AddValidations(this IServiceCollection service)
    {
        service.AddScoped<IColorValidation, ColorValidation>();
        service.AddValidatorsFromAssemblyContaining<ColorCreateValidation>();
    }
}
