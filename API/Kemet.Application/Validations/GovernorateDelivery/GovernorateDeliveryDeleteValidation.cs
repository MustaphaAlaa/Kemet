using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class GovernorateDeliveryDeleteValidation : AbstractValidator<GovernorateDeliveryDeleteDTO>
{
    public GovernorateDeliveryDeleteValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateDeliveryId)
            .NotEmpty()
            .WithMessage("GovernorateDelivery ID is required."); 
    }
}
