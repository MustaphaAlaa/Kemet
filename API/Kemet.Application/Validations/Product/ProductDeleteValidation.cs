using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductDeleteValidation : AbstractValidator<ProductDeleteDTO>
{
    public ProductDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.ProductId).GreaterThanOrEqualTo(1).WithMessage("Product ID must be greater than 0.");
    }
}
