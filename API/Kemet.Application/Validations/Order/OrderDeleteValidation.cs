using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class OrderDeleteValidation : AbstractValidator<OrderDeleteDTO>
{
    public OrderDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.OrderId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Order ID must be greater than 0.");
    }
}
