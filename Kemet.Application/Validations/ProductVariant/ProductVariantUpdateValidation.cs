using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductVariantUpdateValidation : AbstractValidator<ProductVariantUpdateDTO>
{
    public ProductVariantUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.ProductVariantId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Product Variant Id must be greater than 0.");

        RuleFor(x => x.ProductId).GreaterThanOrEqualTo(1).WithMessage("Product Id must be greater than 0.");
        RuleFor(x => x.SizeId).GreaterThanOrEqualTo(1).WithMessage("Size Id must be greater than 0.");
        RuleFor(x => x.ColorId).GreaterThanOrEqualTo(1).WithMessage("Color Id must be greater than 0.");
    }
}
