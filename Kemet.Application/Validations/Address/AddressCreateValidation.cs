using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class AddressCreateValidation : AbstractValidator<AddressCreateDTO>
{
    public AddressCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("Create Address entity is null");

        RuleFor(x => x.CustomerId).GreaterThanOrEqualTo(1).WithMessage("Customer Id not valid");

        RuleFor(x => x.GovernorateId).GreaterThanOrEqualTo(1).WithMessage("Customer Id not valid");

        RuleFor(x => x.StreetAddress).NotEmpty().WithMessage("Street Address is required.");
    }
}
