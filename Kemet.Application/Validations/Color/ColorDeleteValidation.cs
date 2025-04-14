using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ColorDeleteValidation : AbstractValidator<ColorDeleteDTO>
{
    public ColorDeleteValidation()
    {
        RuleFor(x => x.ColorId).NotEmpty().WithMessage("Color ID is required.");
        RuleFor(x => x.ColorId).LessThan(1).WithMessage("Color ID must be greater than 0.");
    }
}
