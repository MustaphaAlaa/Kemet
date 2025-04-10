using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IProductVariantValidation
    : IValidator<ProductVariantCreateDTO, ProductVariantUpdateDTO, ProductVariantDeleteDTO> { }
