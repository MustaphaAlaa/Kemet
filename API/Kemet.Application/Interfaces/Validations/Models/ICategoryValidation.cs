using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface ICategoryValidation
    : IValidator<CategoryCreateDTO, CategoryUpdateDTO, CategoryDeleteDTO> { }
