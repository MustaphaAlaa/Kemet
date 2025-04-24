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
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductQuantityPriceService : SaveService, IProductQuantityPriceService
{
    private readonly IBaseRepository<ProductQuantityPrice> _repository;
    private readonly IProductQuantityPriceValidation _productQuantityPriceValidation;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductQuantityPriceService> _logger;
    private readonly IRepositoryRetrieverHelper<ProductQuantityPrice> _repositoryHelper;

    public ProductQuantityPriceService(
        IProductQuantityPriceValidation productQuantityPriceValidation,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<ProductQuantityPriceService> logger,
        IRepositoryRetrieverHelper<ProductQuantityPrice> repoHelper
    )
        : base(unitOfWork)
    {
        _productQuantityPriceValidation = productQuantityPriceValidation;
        _mapper = mapper;
        _logger = logger;
        _repositoryHelper = repoHelper;
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
                $"An error occurred while creating the product Quantity Price. \n{ex.Message}";
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
            var msg = $"An error occurred while deleting the product Quantity Price.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<ProductQuantityPriceReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<ProductQuantityPriceReadDTO>();
    }

    public async Task<IEnumerable<ProductQuantityPriceReadDTO>> RetrieveAllAsync(
        Expression<Func<ProductQuantityPrice, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<ProductQuantityPriceReadDTO>(predicate);
    }

    public async Task<ProductQuantityPriceReadDTO> RetrieveByAsync(
        Expression<Func<ProductQuantityPrice, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveByAsync<ProductQuantityPriceReadDTO>(predicate);
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

    public async Task<ProductQuantityPriceReadDTO> UpdateInternalAsync(
        ProductQuantityPriceUpdateDTO updateRequest
    )
    {
        try
        {
            var updatedDto = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return updatedDto;
        }
        catch (Exception ex)
        {
            var msg =
                $"An error occurred while updating the product Quantity Price. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
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
                $"An error occurred while updating the product Quantity Price. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
