 using Entities.Models.DTOs;
 using FluentValidation; 

namespace Entities.Models.Validations;

public class GovernorateDeliveryCompanyUpdateValidation : AbstractValidator<GovernorateDeliveryCompanyUpdateDTO>
{
    public GovernorateDeliveryCompanyUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateDeliveryCompanyId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("GovernorateDeliveryCompany ID must be greater than 0.");
 
        RuleFor(x => x.DeliveryCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Delivery cost must be greater than or equal to 0.");
    }
}
