using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class SizeDeleteValidation : AbstractValidator<SizeDeleteDTO>
{
    public SizeDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.SizeId).GreaterThanOrEqualTo(1).WithMessage("Size ID must be greater than 0.");
    }
}
