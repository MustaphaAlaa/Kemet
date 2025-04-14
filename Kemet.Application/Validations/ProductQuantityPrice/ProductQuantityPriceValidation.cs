using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class ProductQuantityPriceValidation : IProductQuantityPriceValidation
{
    private readonly IBaseRepository<ProductQuantityPrice> _repository;
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Price> _priceRepository;

    private readonly ILogger<ProductQuantityPriceValidation> _logger;
    private readonly IValidator<ProductQuantityPriceCreateDTO> _createValidator;
    private readonly IValidator<ProductQuantityPriceUpdateDTO> _updateValidator;
    private readonly IValidator<ProductQuantityPriceDeleteDTO> _deleteValidator;

    public ProductQuantityPriceValidation(
        IBaseRepository<ProductQuantityPrice> repository,
        IBaseRepository<Product> productRepository,
        IBaseRepository<Price> priceRepository,
        ILogger<ProductQuantityPriceValidation> logger,
        IValidator<ProductQuantityPriceCreateDTO> createValidator,
        IValidator<ProductQuantityPriceUpdateDTO> updateValidator,
        IValidator<ProductQuantityPriceDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _productRepository = productRepository;
        _priceRepository = priceRepository;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(ProductQuantityPriceCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            var product = await _productRepository.RetrieveAsync(p =>
                p.ProductId == entity.ProductId
            );

            Utility.DoesExist(product);

            await ValidateUnitPrice(entity.ProductId, entity.UnitPrice);

            var ProductQuantityPrice = await _repository.RetrieveAsync(PQP =>
                PQP.ProductId == entity.ProductId
                && PQP.Quantity == entity.Quantity
                && PQP.UnitPrice == entity.UnitPrice
            );

            Utility.AlreadyExist(ProductQuantityPrice, "ProductQuantityPrice");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the ProductQuantityPrice. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(ProductQuantityPriceDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the ProductQuantityPrice. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(ProductQuantityPriceUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var ProductQuantityPrice = await _repository.RetrieveAsync(PQP =>
                PQP.ProductQuantityPriceId == entity.ProductQuantityPriceId
                && PQP.ProductId == entity.ProductId
                && PQP.Quantity == entity.Quantity
                && PQP.UnitPrice == entity.UnitPrice
            );

            Utility.DoesExist(ProductQuantityPrice, "ProductQuantityPrice");

            await ValidateUnitPrice(entity.ProductId, entity.UnitPrice);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the ProductQuantityPrice. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    private async Task ValidateUnitPrice(int ProductId, decimal UnitPrice)
    {
        var price = await _priceRepository.RetrieveAsync(price => price.ProductId == ProductId);
        Utility.DoesExist(price);

        if (UnitPrice < price?.MinimumPrice)
            throw new ValidationException("the price is lower than the minimum price.");

        if (UnitPrice > price?.MaximumPrice)
            throw new ValidationException("the price is greater than maximum price.");
    }
}
