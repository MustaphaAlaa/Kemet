using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class OrderItemUpdateValidation : AbstractValidator<OrderItemUpdateDTO>
{
    public OrderItemUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.OrderItemId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("OrderItem ID must be greater than 0.");

        RuleFor(x => x.OrderId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Order ID must be greater than 0.");

        RuleFor(x => x.ProductVariantId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Product Variant Id should be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("The Quantity Should be 1 at least.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Unit UnitPrice Should be Greater Than 0.");
    }
}
