using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class GovernorateDeliveryUpdateValidation : AbstractValidator<GovernorateDeliveryUpdateDTO>
{
    public GovernorateDeliveryUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateDeliveryId)
            .GreaterThan(0)
            .WithMessage("GovernorateDelivery ID must be greater than 0.");

        

        RuleFor(x => x.DeliveryCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Delivery cost must be greater than or equal to 0.");
    }
}
