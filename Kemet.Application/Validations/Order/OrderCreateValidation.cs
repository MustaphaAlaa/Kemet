using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class OrderCreateValidation : AbstractValidator<OrderCreateDTO>
{
    public OrderCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.CustomerId)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Customer Id should be greater than 0.");

        //RuleFor(x => x.OrderItems).NotNull().WithMessage("Cannot Create Order for no orders");
    }
}
