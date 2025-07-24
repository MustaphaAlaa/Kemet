using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class OrderItemDeleteValidation : AbstractValidator<OrderItemDeleteDTO>
{
    public OrderItemDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.OrderItemId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("OrderItem ID must be greater than 0.");
    }
}
