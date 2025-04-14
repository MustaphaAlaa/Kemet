using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ColorCreateValidation : AbstractValidator<ColorCreateDTO>
{
    public ColorCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");

        RuleFor(x => x.Hexacode).NotEmpty().WithMessage("Hexacode is required.");
    }
}
