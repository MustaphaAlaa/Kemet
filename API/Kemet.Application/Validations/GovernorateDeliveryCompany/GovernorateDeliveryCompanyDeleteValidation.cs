using Entities.Models.DTOs;
using FluentValidation;  

namespace Entities.Models.Validations;

public class GovernorateDeliveryCompanyDeleteValidation : AbstractValidator<GovernorateDeliveryCompanyDeleteDTO>
{
    public GovernorateDeliveryCompanyDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateDeliveryCompanyId)
            .NotEmpty()
            .WithMessage("GovernorateDeliveryCompany ID is required."); 
    }
}
