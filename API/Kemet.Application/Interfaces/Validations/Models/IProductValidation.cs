using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IProductValidation
    : IValidator<ProductCreateDTO, ProductUpdateDTO, ProductDeleteDTO> { }
