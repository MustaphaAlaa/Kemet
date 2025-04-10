using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IProductValidation
    : IValidator<ProductCreateDTO, ProductUpdateDTO, ProductDeleteDTO> { }
