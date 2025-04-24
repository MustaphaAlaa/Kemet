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

public class ProductService : SaveService, IProductService
{
    private readonly IBaseRepository<Product> _repository;
    private readonly IProductValidation _productValidation;
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
        : base(unitOfWork)
    {
        _productValidation = productValidation;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
        _repository = _unitOfWork.GetRepository<Product>();
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

    public async Task DeleteAsync(ProductDeleteDTO entity)
    {
        try
        {
            await _productValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(p => p.ProductId == entity.ProductId);
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
}
