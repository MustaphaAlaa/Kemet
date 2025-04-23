using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
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

    #region Create
    private async Task<ProductReadDTO> CreateProductCore(ProductCreateDTO entity)
    {
        await _productValidation.ValidateCreate(entity);

        var product = _mapper.Map<Product>(entity);

        product.CreatedAt = DateTime.Now;
        product.UpdatedAt = DateTime.Now;

        product = await _repository.CreateAsync(product);

        var newProduct = _mapper.Map<ProductReadDTO>(product);

        return newProduct;
    }

    public async Task<ProductReadDTO> CreateInternalAsync(ProductCreateDTO entity)
    {
        try
        {
            var productReadDto = await this.CreateProductCore(entity);

            await _unitOfWork.SaveChangesAsync();

            return productReadDto;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"Product is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO entity)
    {
        try
        {
            var newProduct = await CreateProductCore(entity);

            return newProduct;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (AlreadyExistException ex)
        {
            string msg = $"Product is already exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    #endregion





    #region  Update

    private async Task<ProductReadDTO> UpdateProductCore(ProductUpdateDTO updateRequest)
    {
        await _productValidation.ValidateUpdate(updateRequest);

        var productToUpdate = _mapper.Map<Product>(updateRequest);

        productToUpdate.UpdatedAt = DateTime.Now;

        var updatedProduct = _repository.Update(productToUpdate);

        return _mapper.Map<ProductReadDTO>(updatedProduct);
    }

    public async Task<ProductReadDTO> UpdateInternalAsync(ProductUpdateDTO updateRequest)
    {
        try
        {
            var updatedDto = await this.UpdateProductCore(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return updatedDto;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Product doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> Update(ProductUpdateDTO updateRequest)
    {
        try
        {
            var updatedDto = await this.UpdateProductCore(updateRequest);
            return updatedDto;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Product doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
    #endregion





    #region Delete
    private async Task DeleteProductCore(ProductDeleteDTO entity)
    {
        await _productValidation.ValidateDelete(entity);

        await _repository.DeleteAsync(p => p.ProductId == entity.ProductId);
    }

    public async Task DeleteAsync(ProductDeleteDTO entity)
    {
        try
        {
            await DeleteProductCore(entity);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<bool> DeleteInternalAsync(ProductDeleteDTO entity)
    {
        try
        {
            await DeleteProductCore(entity);

            bool isDeleted = await _unitOfWork.SaveChangesAsync() > 0;

            return isDeleted;
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the product. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    #endregion





    #region  Retrieve


    public async Task<List<ProductReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<ProductReadDTO>();
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving product records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<ProductReadDTO>> RetrieveAllAsync(
        Expression<Func<Product, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<ProductReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving product records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> RetrieveByAsync(Expression<Func<Product, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<ProductReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving the product record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.ProductId == key);

    }
    #endregion
}
