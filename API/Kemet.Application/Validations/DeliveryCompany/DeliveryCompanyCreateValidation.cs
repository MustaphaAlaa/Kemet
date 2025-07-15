using Entities.Models.DTOs;
using FluentValidation;

namespace Entities.Models.Validations;

public class DeliveryCompanyCreateValidation : AbstractValidator<DeliveryCompanyCreateDTO>
{
    public DeliveryCompanyCreateValidation()
    {
        RuleFor(x => x).NotNull().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}
