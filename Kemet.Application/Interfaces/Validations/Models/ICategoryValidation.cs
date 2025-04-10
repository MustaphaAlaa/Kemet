using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface ICategoryValidation
    : IValidator<CategoryCreateDTO, CategoryUpdateDTO, CategoryDeleteDTO> { }
