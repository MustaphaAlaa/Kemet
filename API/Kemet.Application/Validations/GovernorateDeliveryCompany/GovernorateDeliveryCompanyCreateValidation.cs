 using Entities.Models.DTOs;
 using FluentValidation; 
 namespace Entities.Models.Validations;

public class GovernorateDeliveryCompanyCreateValidation : AbstractValidator<GovernorateDeliveryCompanyCreateDTO>
{
    public GovernorateDeliveryCompanyCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Governorate ID must be greater than 0.");
        
        RuleFor(x => x.DeliveryCompanyId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("DeliveryCompany ID must be greater than 0.");

      
    }
}
