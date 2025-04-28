using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductVariantService
    : GenericService<ProductVariant, ProductVariantReadDTO>,
        IProductVariantService
{
    private readonly IBaseRepository<ProductVariant> _repository;
    private readonly IProductVariantValidation _ProductVariantValidation;

    public ProductVariantService(
        IProductVariantValidation ProductVariantValidation,
        ServiceFacade_DependenceInjection<ProductVariant> facade
    )
        : base(facade, "Product-variant")
    {
        _ProductVariantValidation = ProductVariantValidation;
        _repository = _unitOfWork.GetRepository<ProductVariant>();
    }

    public async Task<ProductVariantReadDTO> CreateAsync(ProductVariantCreateDTO entity)
    {
        try
        {
            await _ProductVariantValidation.ValidateCreate(entity);

            var ProductVariant = _mapper.Map<ProductVariant>(entity);

            ProductVariant = await _repository.CreateAsync(ProductVariant);

            return _mapper.Map<ProductVariantReadDTO>(ProductVariant);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task DeleteAsync(ProductVariantDeleteDTO entity)
    {
        try
        {
            await _ProductVariantValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(p => p.ProductVariantId == entity.ProductVariantId);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the {TName}.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<ProductVariantReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.ProductVariantId == key);
    }

    public async Task<ProductVariantReadDTO> Update(ProductVariantUpdateDTO updateRequest)
    {
        try
        {
            await _ProductVariantValidation.ValidateUpdate(updateRequest);

            var ProductVariantToUpdate = _mapper.Map<ProductVariant>(updateRequest);

            ProductVariantToUpdate = _repository.Update(ProductVariantToUpdate);

            return _mapper.Map<ProductVariantReadDTO>(ProductVariantToUpdate);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

    public async Task<List<ProductVariantReadDTO>> AddRange(
        IEnumerable<ProductVariantCreateDTO> productVariantCreateDTOs
    )
    {
        try
        {
            List<ProductVariantReadDTO> productVariants = new();

            foreach (var productVariant in productVariantCreateDTOs)
            {
                var productVariantRead = await this.CreateAsync(productVariant);
                productVariants.Add(productVariantRead);
            }

            return productVariants;
        }
        catch (FailedToCreateException ex)
        {
            string msg = $"Failed to add a list of {TName}s in the database. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while adding a list of {TName}s. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<bool> CheckProductVariantAvailability(int productVariantId)
    {
        try
        {
            var productVariant = await this.RetrieveByAsync(pv =>
                pv.ProductVariantId == productVariantId && pv.StockQuantity > 0
            );

            return productVariant != null;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while checking the {TName} availability. \n{ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<bool> CheckProductVariantAvailability(int productVariantId, int Quantity)
    {
        try
        {
            var productVariant = await this.RetrieveByAsync(pv =>
                pv.ProductVariantId == productVariantId && pv.StockQuantity >= Quantity
            );

            return productVariant != null;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while checking the {TName} availability. \n{ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
