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
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService : GenericService<Product, ProductReadDTO, ProductService>, IProductService
{
    private readonly IBaseRepository<Product> _repository;
    private readonly IProductValidation _productValidation;

    public ProductService(
        IServiceFacade_DependenceInjection<Product, ProductService> facade,
        IProductValidation productValidation)
        : base(facade, "Product")
    {
        _productValidation = productValidation;
        _repository = _unitOfWork.GetRepository<Product>();
    }

    public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO entity)
    {
        try
        {
            await _productValidation.ValidateCreate(entity);

            var product = _mapper.Map<Product>(entity);

            product.CreatedAt = DateTime.UtcNow;  // switching to UTCNow because Postgresql should now about TimeZone
            product.UpdatedAt = DateTime.UtcNow; //

            product = await _repository.CreateAsync(product);
            await this.SaveAsync();
            var newProduct = _mapper.Map<ProductReadDTO>(product);

            return newProduct;
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the {TName}. {ex.Message}";
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
                $"An error thrown while validating the creation of the {TName}. {ex.Message}";
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
            string msg = $"Validating Exception is thrown while updating the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"{TName} doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the {TName}. {ex.Message}";
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
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<ProductReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.ProductId == key);
    }
}
