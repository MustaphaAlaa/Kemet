using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class AddressDeleteValidation : AbstractValidator<AddressDeleteDTO>
{
    public AddressDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.AddressId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Address ID must be greater than 0.");
    }
}
