using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductQuantityPriceUpdateValidation : AbstractValidator<ProductQuantityPriceUpdateDTO>
{
    public ProductQuantityPriceUpdateValidation()
    {
        RuleFor(x => x.ProductQuantityPriceId).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Quantity).LessThan(1).WithMessage("At Least Quantity should be 1.");
        RuleFor(x => x.UnitPrice)
            .LessThanOrEqualTo(0)
            .WithMessage("Price cannot be 0 or less than 0.");
    }
}
