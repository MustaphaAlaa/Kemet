using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductQuantityPriceService
    : GenericService<
        ProductQuantityPrice,
        ProductQuantityPriceReadDTO,
        ProductQuantityPriceService
    >,
        IProductQuantityPriceService
{
    private readonly IBaseRepository<ProductQuantityPrice> _repository;
    private readonly IProductQuantityPriceValidation _productQuantityPriceValidation;

    public ProductQuantityPriceService(
        IServiceFacade_DependenceInjection<
            ProductQuantityPrice,
            ProductQuantityPriceService
        > facade,
        IProductQuantityPriceValidation productQuantityPriceValidation
    )
        : base(facade, "Product-Quantity-UnitPrice")
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




            productQuantityPrice.CreatedAt = DateTime.UtcNow;
            productQuantityPrice.IsActive = true;

            productQuantityPrice = await _repository.CreateAsync(productQuantityPrice);


            var newProductQuantityPrice = _mapper.Map<ProductQuantityPriceReadDTO>(
                productQuantityPrice
            );

            return newProductQuantityPrice;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }

    public async Task<List<ProductQuantityPriceReadDTO>> AddRange(
        IEnumerable<ProductQuantityPriceCreateDTO> productQuantityPriceCreateDTOs
    )
    {
        List<ProductQuantityPriceReadDTO> PQPReadDTOs = new();
        foreach (var PQP in productQuantityPriceCreateDTOs)
        {
            var Pqp = await this.CreateAsync(PQP);

            PQPReadDTOs.Add(Pqp);
        }
        return PQPReadDTOs;
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

    public async Task<IEnumerable<ProductQuantityPriceReadDTO>> ActiveQuantityPriceFor(
        int ProductId
    )
    {
        var dTOs = await this.RetrieveAllAsync(entity =>
            (entity.ProductId == ProductId) && entity.IsActive == true
        );

        return dTOs.ToList();
    }

    public async Task<ProductQuantityPriceReadDTO> ActiveProductPriceForQuantityWithId(
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
            var msg = $"An error occurred while updating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }

    public async Task<ProductQuantityPriceReadDTO> Deactivate(int ProductId, int ProductQuantityPriceId)
    {


        try
        {
            var productQuantityPriceDTO = await this.RetrieveByAsync(pv => pv.ProductQuantityPriceId == ProductQuantityPriceId &&
                                                                     pv.ProductId == ProductId);

            var productQuantityPrice = _mapper.Map<ProductQuantityPrice>(productQuantityPriceDTO);
            productQuantityPrice.IsActive = false;


            var UpdatedProductQuantityPrice = _repository.Update(productQuantityPrice);

            var pr = _mapper.Map<ProductQuantityPriceReadDTO>(UpdatedProductQuantityPrice);

            return await Task.FromResult(pr);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the {TName}. \n{ex.Message}";
            _logger.LogError(msg);
            // throw new FailedToCreateException(msg);
            throw;
        }
    }



}
