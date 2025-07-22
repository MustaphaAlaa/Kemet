using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class CustomerCreateValidation : AbstractValidator<CustomerCreateDTO>
{
    public CustomerCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("Customer entity is null");

        RuleFor(x => x)
            .Must(x =>
                !(
                    (x.UserId == Guid.Empty || x.UserId == null)
                    && String.IsNullOrEmpty(x.FirstName)
                    && String.IsNullOrEmpty(x.LastName)
                    && String.IsNullOrEmpty(x.PhoneNumber)
                )
            )
            .WithMessage("All Customer data is empty or null or invalid");

        RuleFor(x => x)
            .Must(x =>
                !(
                    x.UserId == Guid.Empty
                    && (
                        string.IsNullOrEmpty(x.FirstName)
                        || string.IsNullOrEmpty(x.LastName)
                        || string.IsNullOrEmpty(x.PhoneNumber)
                    )
                )
            )
            .WithMessage("Anonymous user data is empty or null");
    }
}
