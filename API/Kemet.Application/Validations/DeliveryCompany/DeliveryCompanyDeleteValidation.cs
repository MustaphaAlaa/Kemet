using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class DeliveryCompanyDeleteValidation : AbstractValidator<DeliveryCompanyDeleteDTO>
{
    public DeliveryCompanyDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");
        RuleFor(x => x.DeliveryCompanyId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("DeliveryCompany ID must be greater than 0.");
    }
}
