using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductVariantCreateValidation : AbstractValidator<ProductVariantCreateDTO>
{
    public ProductVariantCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product Id must be greater than 0.");
        RuleFor(x => x.SizeId).GreaterThan(0).WithMessage("Size Id must be greater than 0.");
        RuleFor(x => x.ColorId).GreaterThan(0).WithMessage("Color Id must be greater than 0.");
    }
}
