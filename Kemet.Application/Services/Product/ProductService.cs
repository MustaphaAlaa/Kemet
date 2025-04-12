using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces.Helpers;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService : IProductService
{
    private readonly IBaseRepository<Product> _repository;
    private readonly IProductValidation _productValidation;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductService> _logger;
    private readonly IRepositoryRetrieverHelper<Product> _repositoryHelper;

    public ProductService(
        IProductValidation productValidation,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ProductService> logger,
        IRepositoryRetrieverHelper<Product> repoHelper
    )
    {
        _productValidation = productValidation;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
        _repository = _unitOfWork.GetRepository<Product>();
    }

    public async Task<ProductReadDTO> CreateInternalAsync(ProductCreateDTO entity)
    {
        try
        {
            var productReadDto = await this.CreateAsync(entity);

            await _unitOfWork.SaveChangesAsync();

            return productReadDto;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO entity)
    {
        try
        {
            await _productValidation.ValidateCreate(entity);

            var product = _mapper.Map<Product>(entity);

            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            product = await _repository.CreateAsync(product);

            var newProduct = _mapper.Map<ProductReadDTO>(product);

            return newProduct;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(ProductDeleteDTO entity)
    {
        try
        {
            await _productValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(p => p.ProductId == entity.ProductId);

            bool isDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the product.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<ProductReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<ProductReadDTO>();
    }

    public async Task<IEnumerable<ProductReadDTO>> RetrieveAllAsync(
        Expression<Func<Product, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<ProductReadDTO>(predicate);
    }

    public async Task<ProductReadDTO> RetrieveByAsync(Expression<Func<Product, bool>> predicate)
    {
        return await _repositoryHelper.RetrieveByAsync<ProductReadDTO>(predicate);
    }

    public async Task<ProductReadDTO> UpdateInternalAsync(ProductUpdateDTO updateRequest)
    {
        try
        {
            var updatedDto = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return updatedDto;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> Update(ProductUpdateDTO updateRequest)
    {
        try
        {
            await _productValidation.ValidateUpdate(updateRequest);

            var productToUpdate = _mapper.Map<Product>(updateRequest);

            productToUpdate.UpdatedAt = DateTime.Now;

            var updatedProduct = _repository.Update(productToUpdate);

            return _mapper.Map<ProductReadDTO>(updatedProduct);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
