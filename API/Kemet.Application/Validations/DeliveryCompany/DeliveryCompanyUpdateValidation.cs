using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class DeliveryCompanyUpdateValidation : AbstractValidator<DeliveryCompanyUpdateDTO>
{
    public DeliveryCompanyUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.DeliveryCompanyId).NotEmpty().WithMessage("DeliveryCompany ID is required.");
        RuleFor(x => x.DeliveryCompanyId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("DeliveryCompany ID must be greater than 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Delivery Company Name is required.");
    }
}
