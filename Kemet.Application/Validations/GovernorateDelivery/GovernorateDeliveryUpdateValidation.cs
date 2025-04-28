using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class GovernorateDeliveryUpdateValidation : AbstractValidator<GovernorateDeliveryUpdateDTO>
{
    public GovernorateDeliveryUpdateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateDeliveryId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("GovernorateDelivery ID must be greater than 0.");

        RuleFor(x => x.GovernorateId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Governorate ID must be greater than 0.");

        RuleFor(x => x.DeliveryCost)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Delivery cost must be greater than or equal to 0.");
    }
}
