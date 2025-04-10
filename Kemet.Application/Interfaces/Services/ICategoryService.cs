using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface ICategoryService
    : IServiceAsync<Category, CategoryDeleteDTO, CategoryUpdateDTO, CategoryReadDTO> { }
