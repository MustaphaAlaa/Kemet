using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IProductVariantValidation
    : IValidator<ProductVariantCreateDTO, ProductVariantUpdateDTO, ProductVariantDeleteDTO> { }
