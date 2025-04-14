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
        service.AddValidatorsFromAssemblyContaining<ColorCreateValidation>();
    }
}
