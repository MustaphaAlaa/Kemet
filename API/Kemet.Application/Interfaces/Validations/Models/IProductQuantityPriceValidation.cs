using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IProductQuantityPriceValidation
    : IValidator<
        ProductQuantityPriceCreateDTO,
        ProductQuantityPriceUpdateDTO,
        ProductQuantityPriceDeleteDTO
    > { }
