using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class AddressUpdateValidation : AbstractValidator<AddressUpdateDTO>
{
    public AddressUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("Update Address entity is null");

        RuleFor(x => x.AddressId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Address ID must be greater than 0.");

        RuleFor(x => x.CustomerId).NotEqual(Guid.Empty).WithMessage("Customer Id not valid");

        RuleFor(x => x.GovernorateId).GreaterThanOrEqualTo(1).WithMessage("Customer Id not valid");

        RuleFor(x => x.StreetAddress).NotEmpty().WithMessage("Street Address is required.");
    }
}
