using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductVariantCreateValidation : AbstractValidator<ProductVariantCreateDTO>
{
    public ProductVariantCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.ProductId).LessThan(1).WithMessage("Product Id must be greater than 0.");
        RuleFor(x => x.SizeId).LessThan(1).WithMessage("Size Id must be greater than 0.");
        RuleFor(x => x.ColorId).LessThan(1).WithMessage("Color Id must be greater than 0.");
    }
}
