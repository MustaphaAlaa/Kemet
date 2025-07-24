using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class PriceCreateValidation : AbstractValidator<PriceCreateDTO>
{
    public PriceCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.ProductId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Product Id must be greater than 0.");
        RuleFor(x => x.MinimumPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Minimum price must be greater than 0.");
        RuleFor(x => x.MaximumPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Maximum price must be greater than 0.");
    }
}
