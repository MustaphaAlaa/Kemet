using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductDeleteValidation : AbstractValidator<ProductDeleteDTO>
{
    public ProductDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.ProductId).LessThan(1).WithMessage("Product ID must be greater than 0.");
    }
}
