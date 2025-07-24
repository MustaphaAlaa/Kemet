using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class GovernorateDeliveryCreateValidation : AbstractValidator<GovernorateDeliveryCreateDTO>
{
    public GovernorateDeliveryCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.GovernorateId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Governorate ID must be greater than 0.");

        RuleFor(x => x.DeliveryCost)
            .NotNull()
            .GreaterThanOrEqualTo(0)
            .WithMessage("Delivery cost must be greater than or equal to 0.");

        RuleFor(x => x.IsActive).NotNull().WithMessage("IsActive Should be true or false");
    }
}
