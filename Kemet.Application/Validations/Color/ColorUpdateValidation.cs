using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ColorUpdateValidation : AbstractValidator<ColorUpdateDTO>
{
    public ColorUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.ColorId).NotEmpty().WithMessage("Color ID is required.");
        RuleFor(x => x.ColorId).LessThan(1).WithMessage("Color ID must be greater than 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.HexaCode).NotEmpty().WithMessage("HexaCode is required.");
    }
}
