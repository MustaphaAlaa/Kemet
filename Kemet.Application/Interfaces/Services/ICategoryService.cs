using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ICategoryService
    : IServiceAsync<
        Category,
        CategoryCreateDTO,
        CategoryDeleteDTO,
        CategoryUpdateDTO,
        CategoryReadDTO
    > { }
