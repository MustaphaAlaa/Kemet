using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductQuantityPriceDeleteValidation : AbstractValidator<ProductQuantityPriceDeleteDTO>
{
    public ProductQuantityPriceDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.ProductQuantityPriceId)
            .NotEmpty()
            .WithMessage("ProductQuantityPrice ID is required.");
    }
}
