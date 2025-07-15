using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductVariantDeleteValidation : AbstractValidator<ProductVariantDeleteDTO>
{
    public ProductVariantDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.ProductVariantId)
            .LessThan(1)
            .WithMessage("ProductVariant ID must be greater than 0.");
    }
}
