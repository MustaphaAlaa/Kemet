using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class CustomerDeleteValidation : AbstractValidator<CustomerDeleteDTO>
{
    public CustomerDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("Customer entity is null");

        RuleFor(x => x.CustomerId).LessThan(1).WithMessage("Customer ID must be greater than 0.");
    }
}
