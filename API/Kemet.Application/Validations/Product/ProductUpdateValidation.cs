using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductUpdateValidation : AbstractValidator<ProductUpdateDTO>
{
    public ProductUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.ProductId).GreaterThanOrEqualTo(1).WithMessage("Product ID must be greater than 0.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(0).WithMessage("Category ID must be greater than 0.");

    }
}
