using Application.Services;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;

namespace Kemet.Application.Services.Models
{
    public class CategoryService
        : GenericService<Category, CategoryReadDTO, CategoryService>,
            ICategoryService
    {
        private readonly IBaseRepository<Category> _repository;

        public CategoryService(
            IServiceFacade_DependenceInjection<Category, CategoryService> facadeDI
        )
            : base(facadeDI, "Category")
        {
            _repository = _unitOfWork.GetRepository<Category>();

        }

        public Task<CategoryReadDTO> CreateAsync(CategoryCreateDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(CategoryDeleteDTO entity)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryReadDTO> GetById(int key)
        {
            return _repositoryHelper.RetrieveByAsync<CategoryReadDTO>(c => c.CategoryId == key);
        }

        public Task<CategoryReadDTO> Update(CategoryUpdateDTO updateRequest)
        {
            throw new NotImplementedException();
        }
    }
}
