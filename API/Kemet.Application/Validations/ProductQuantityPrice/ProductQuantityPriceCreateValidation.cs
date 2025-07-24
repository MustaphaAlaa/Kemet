using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductQuantityPriceCreateValidation : AbstractValidator<ProductQuantityPriceCreateDTO>
{
    public ProductQuantityPriceCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1).WithMessage("At Least Quantity should be 1.");

        RuleFor(x => x.UnitPrice)
            .GreaterThanOrEqualTo(1)
            .WithMessage("UnitPrice cannot be 0 or less than 0.");
    }
}
