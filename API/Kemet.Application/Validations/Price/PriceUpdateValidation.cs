using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class PriceUpdateValidation : AbstractValidator<PriceUpdateDTO>
{
    public PriceUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.PriceId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("UnitPrice Id must be greater than 0.");
        RuleFor(x => x.MinimumPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Minimum price must be greater than 0.");
        RuleFor(x => x.MaximumPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Maximum price must be greater than 0.");
    }
}
