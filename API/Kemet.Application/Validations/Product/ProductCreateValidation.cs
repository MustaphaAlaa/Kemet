using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class ProductCreateValidation : AbstractValidator<ProductCreateDTO>
{
    public ProductCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.CategoryId).GreaterThanOrEqualTo(0).WithMessage("Category Id should be greater than 0.");
    }
}
