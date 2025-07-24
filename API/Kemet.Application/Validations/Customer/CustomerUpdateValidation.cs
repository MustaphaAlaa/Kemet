using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class CustomerUpdateValidation : AbstractValidator<CustomerUpdateDTO>
{
    public CustomerUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("Customer entity is null");

        RuleFor(x =>
                x.UserId != Guid.Empty
                && String.IsNullOrEmpty(x.FirstName)
                && String.IsNullOrEmpty(x.LastName)
                && String.IsNullOrEmpty(x.PhoneNumber)
            )
            .NotEmpty()
            .WithMessage("All Customer data is empty or null or invalid");

        RuleFor(x =>
                x.UserId == Guid.Empty
                && (
                    String.IsNullOrEmpty(x.FirstName)
                    || String.IsNullOrEmpty(x.LastName)
                    || String.IsNullOrEmpty(x.PhoneNumber)
                )
            )
            .NotEmpty()
            .WithMessage("Anonymous user data is empty or null");
    }
}
