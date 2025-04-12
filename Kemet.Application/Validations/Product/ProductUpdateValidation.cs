using Entities.Models.DTOs;
using FluentValidation;

namespace Kemet.Application.Validations;

public class ProductUpdateValidation : AbstractValidator<ProductUpdateDTO>
{
    public ProductUpdateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");
        RuleFor(x => x.ProductId).LessThan(1).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.CategoryId).LessThan(0).WithMessage("Category Id is required.");
    }
}
