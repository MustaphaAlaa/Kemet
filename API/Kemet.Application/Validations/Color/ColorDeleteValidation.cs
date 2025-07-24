using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ColorDeleteValidation : AbstractValidator<ColorDeleteDTO>
{
    public ColorDeleteValidation()
    {
        RuleFor(x => x).NotNull().NotEmpty();
        RuleFor(x => x.ColorId).GreaterThanOrEqualTo(1).WithMessage("Color ID must be greater than 0.");
    }
}
