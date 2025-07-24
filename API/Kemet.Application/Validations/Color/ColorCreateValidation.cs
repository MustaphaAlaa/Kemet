using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ColorCreateValidation : AbstractValidator<ColorCreateDTO>
{
    public ColorCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.HexaCode).NotEmpty().WithMessage("HexaCode is required.");
    }
}
