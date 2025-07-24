using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ICategoryService
    : IServiceAsync<
        Category,
        int,
        CategoryCreateDTO,
        CategoryDeleteDTO,
        CategoryUpdateDTO,
        CategoryReadDTO
    >
{ }
