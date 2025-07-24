using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class OrderUpdateValidation : AbstractValidator<OrderUpdateDTO>
{
    public OrderUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.OrderId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Order ID must be greater than 0.");

        RuleFor(x => x.OrderItems).NotNull().WithMessage("Cannot Create Order for no orders");

        RuleFor(x => x.CustomerId);
    }
}
