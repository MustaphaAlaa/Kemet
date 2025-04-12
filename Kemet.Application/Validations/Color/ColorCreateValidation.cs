using Entities.Models.DTOs;
using FluentValidation;

namespace Kemet.Application.Validations;

public class ColorCreateValidation : AbstractValidator<ColorCreateDTO>
{
    public ColorCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.NameAr).NotEmpty().WithMessage("Arabic name is required.");

        RuleFor(x => x.NameEn).NotEmpty().WithMessage("English name is required.");

        RuleFor(x => x.Hexacode).NotEmpty().WithMessage("Hexacode is required.");
    }
}
