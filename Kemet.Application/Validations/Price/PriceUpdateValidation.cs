using Entities.Models.DTOs;
using FluentValidation;

namespace Kemet.Application.Validations;

public class PriceUpdateValidation : AbstractValidator<PriceUpdateDTO>
{
    public PriceUpdateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");
        RuleFor(x => x.PriceId).LessThan(1).WithMessage("Price Id must be greater than 0.");
        RuleFor(x => x.MinimumPrice)
            .LessThan(1)
            .WithMessage("Minimum price must be greater than 0.");
        RuleFor(x => x.MaximumPrice)
            .LessThan(1)
            .WithMessage("Maximum price must be greater than 0.");
    }
}
