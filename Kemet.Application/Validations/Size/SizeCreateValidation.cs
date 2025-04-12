using Entities.Models.DTOs;
using FluentValidation;

namespace Kemet.Application.Validations;

public class SizeCreateValidation : AbstractValidator<SizeCreateDTO>
{
    public SizeCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
