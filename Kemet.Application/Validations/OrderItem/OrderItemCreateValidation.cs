using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class OrderItemCreateValidation : AbstractValidator<OrderItemCreateDTO>
{
    public OrderItemCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.ProductVariantId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Product Variant Id should be greater than 0.");

        RuleFor(x => x.OrderId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Order ID must be greater than 0.");

        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1)
            .WithMessage("The Quantity Should be 1 at least.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Unit Price Should be Greater Than 0.");
    }
}
