using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class PriceDeleteValidation : AbstractValidator<PriceDeleteDTO>
{
    public PriceDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");
        RuleFor(x => x.PriceId).LessThan(1).WithMessage("UnitPrice ID must be greater than 0.");
    }
}
