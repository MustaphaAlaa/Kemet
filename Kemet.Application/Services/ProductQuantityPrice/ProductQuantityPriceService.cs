using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using IRepository.Generic;
using IServices;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductQuantityPriceService : GenericService<ProductQuantityPrice, ProductQuantityPriceReadDTO>,
    IProductQuantityPriceService
{
    private readonly IBaseRepository<ProductQuantityPrice> _repository;
    private readonly IProductQuantityPriceValidation _productQuantityPriceValidation;

    public ProductQuantityPriceService(
        IProductQuantityPriceValidation productQuantityPriceValidation,
        ServiceFacade_DependenceInjection<ProductQuantityPrice> facade
    )
        : base(facade, "Product-Quantity-Price")
    {
        _productQuantityPriceValidation = productQuantityPriceValidation;
        _repository = _unitOfWork.GetRepository<ProductQuantityPrice>();
    }

    public async Task<ProductQuantityPriceReadDTO> CreateAsync(ProductQuantityPriceCreateDTO entity)
    {
        try
        {
            await _productQuantityPriceValidation.ValidateCreate(entity);

            var productQuantityPrice = _mapper.Map<ProductQuantityPrice>(entity);

            productQuantityPrice.CreatedAt = DateTime.Now;
            productQuantityPrice.IsActive = true;

            productQuantityPrice = await _repository.CreateAsync(productQuantityPrice);

            var newProductQuantityPrice = _mapper.Map<ProductQuantityPriceReadDTO>(
                productQuantityPrice
            );

            return newProductQuantityPrice;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while creating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task AddRange(
        IEnumerable<ProductQuantityPriceCreateDTO> productQuantityPriceCreateDTOs
    )
    {
        foreach (var PQP in productQuantityPriceCreateDTOs)
            await this.CreateAsync(PQP);
    }

    public async Task DeleteAsync(ProductQuantityPriceDeleteDTO entity)
    {
        try
        {
            await _productQuantityPriceValidation.ValidateDelete(entity);

            var PQP = await _repository.RetrieveAsync(p =>
                p.ProductQuantityPriceId == entity.ProductQuantityPriceId
            );

            Utility.DoesExist(PQP);

            PQP.IsActive = false;

            _repository.Update(PQP);
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the {TName}.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }


    public async Task<ProductQuantityPriceReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.ProductQuantityPriceId == key);
    }

    public async Task<IEnumerable<ProductQuantityPriceReadDTO>> ActiveQunatityPriceForPrdouctWithId(
        int ProductId
    )
    {
        var dTOs = await this.RetrieveAllAsync(entity =>
            (entity.ProductId == ProductId) && entity.IsActive == true
        );

        return dTOs.ToList();
    }

    public async Task<ProductQuantityPriceReadDTO> ActiveProductPriceForQunatityWithId(
        int ProductId,
        int Quantity
    )
    {
        var dto = await this.RetrieveByAsync(entity =>
            (entity.Quantity == Quantity)
            && (entity.ProductId == ProductId)
            && entity.IsActive == true
        );

        return dto;
    }



    public async Task<ProductQuantityPriceReadDTO> Update(
        ProductQuantityPriceUpdateDTO updateRequest
    )
    {
        try
        {
            await _productQuantityPriceValidation.ValidateUpdate(updateRequest);

            var productQuantityPriceToUpdate = _mapper.Map<ProductQuantityPrice>(updateRequest);

            var updatedProductQuantityPrice = _repository.Update(
                new ProductQuantityPrice
                {
                    ProductQuantityPriceId = updateRequest.ProductQuantityPriceId,
                    IsActive = false,
                }
            );

            var newProductQuantityPrice = _mapper.Map<ProductQuantityPriceCreateDTO>(updateRequest);
            var productQuantityPrice = await this.CreateAsync(newProductQuantityPrice);

            return _mapper.Map<ProductQuantityPriceReadDTO>(productQuantityPrice);
        }
        catch (Exception ex)
        {
            var msg =
                $"An error occurred while updating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
