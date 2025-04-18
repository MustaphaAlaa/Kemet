using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class SizeCreateValidation : AbstractValidator<SizeCreateDTO>
{
    public SizeCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
