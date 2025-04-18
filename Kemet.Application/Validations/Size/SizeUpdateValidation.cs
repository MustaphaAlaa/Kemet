using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class SizeUpdateValidation : AbstractValidator<SizeUpdateDTO>
{
    public SizeUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.SizeId).GreaterThanOrEqualTo(1).WithMessage("Size ID must be greater than 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
