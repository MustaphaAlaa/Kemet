using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using FluentValidation.Validators;
using IRepository.Generic;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class PriceValidation : IPriceValidation
{
    private readonly IBaseRepository<Price> _priceRepository;
    private readonly IBaseRepository<Product> _productRepository;

    private readonly ILogger<PriceValidation> _logger;
    private readonly IValidator<PriceCreateDTO> _createValidator;
    private readonly IValidator<PriceUpdateDTO> _updateValidator;
    private readonly IValidator<PriceDeleteDTO> _deleteValidator;

    public PriceValidation(
        IBaseRepository<Price> PriceRepository,
        IBaseRepository<Product> ProductRepository,
        ILogger<PriceValidation> logger,
        IValidator<PriceCreateDTO> createValidator,
        IValidator<PriceUpdateDTO> updateValidator,
        IValidator<PriceDeleteDTO> deleteValidator
    )
    {
        _productRepository = ProductRepository;
        _priceRepository = PriceRepository;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(PriceCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            if (entity?.StartFrom != null && entity?.EndsAt != null)
            {
                if (entity.StartFrom > entity.EndsAt)
                    throw new ValidationException("Start date must be less than end date.");
                if (entity.StartFrom < DateTime.Now)
                    throw new ValidationException("Start date must be greater than current date.");
            }

            if (entity?.MaximumPrice < entity?.MinimumPrice)
                throw new ValidationException("Maximum price must be greater than minimum price.");

            var product = await _productRepository.RetrieveAsync(g =>
                g.ProductId == entity.ProductId
            );


            Utility.DoesExist(product, "Product");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the UnitPrice. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(PriceDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the UnitPrice. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(PriceUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            if (entity?.StartFrom != null && entity?.EndsAt != null)
            {
                if (entity.StartFrom > entity.EndsAt)
                    throw new ValidationException("Start date must be less than end date.");
                if (entity.StartFrom < DateTime.Now)
                    throw new ValidationException("Start date must be greater than current date.");
            }

            if (entity?.MaximumPrice < entity?.MinimumPrice)
                throw new ValidationException("Maximum price must be greater than minimum price.");

            var Price = await _priceRepository.RetrieveAsync(price =>
                price.PriceId == entity.PriceId
            );

            Utility.DoesExist(Price, "UnitPrice");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the UnitPrice. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
