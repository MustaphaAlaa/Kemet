using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class PriceCreateValidation : AbstractValidator<PriceCreateDTO>
{
    public PriceCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");
        RuleFor(x => x.ProductId).LessThan(1).WithMessage("Product Id must be greater than 0.");
        RuleFor(x => x.MinimumPrice)
            .LessThan(1)
            .WithMessage("Minimum price must be greater than 0.");
        RuleFor(x => x.MaximumPrice)
            .LessThan(1)
            .WithMessage("Maximum price must be greater than 0.");
    }
}
