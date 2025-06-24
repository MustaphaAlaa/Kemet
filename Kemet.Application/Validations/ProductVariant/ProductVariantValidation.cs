using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using IRepository.Generic;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class ProductVariantValidation : IProductVariantValidation
{
    private readonly IBaseRepository<ProductVariant> _productVariantRepository;
    private readonly IBaseRepository<Product> _productRepository;
    private readonly IBaseRepository<Color> _colorRepository;
    private readonly IBaseRepository<Size> _sizeRepository;

    private readonly IValidator<ProductVariantCreateDTO> _createValidator;
    private readonly IValidator<ProductVariantUpdateDTO> _updateValidator;
    private readonly IValidator<ProductVariantDeleteDTO> _deleteValidator;

    private readonly ILogger<ProductVariantValidation> _logger;

    public ProductVariantValidation(
        IBaseRepository<ProductVariant> productVariantRepository,
        IBaseRepository<Product> productRepository,
        IBaseRepository<Color> colorRepository,
        IBaseRepository<Size> sizeRepository,
        IValidator<ProductVariantCreateDTO> createValidator,
        IValidator<ProductVariantUpdateDTO> updateValidator,
        IValidator<ProductVariantDeleteDTO> deleteValidator,
        ILogger<ProductVariantValidation> logger
    )
    {
        _productVariantRepository = productVariantRepository;
        _productRepository = productRepository;
        _colorRepository = colorRepository;
        _sizeRepository = sizeRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
        _logger = logger;
    }

    public async Task ValidateCreate(ProductVariantCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            var product = await _productRepository.RetrieveAsync(p =>
                p.ProductId == entity.ProductId
            );
            Utility.DoesExist(product, "Product");

            var size = await _sizeRepository.RetrieveAsync(s => s.SizeId == entity.SizeId);
            Utility.DoesExist(size, "Size");

            var color = await _colorRepository.RetrieveAsync(c => c.ColorId == entity.ColorId);

            Utility.DoesExist(color, "Color");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the product variant. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(ProductVariantDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the delete request of the product variant. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(ProductVariantUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var ProductVariant = await _productVariantRepository.RetrieveAsync(p =>
                p.ProductVariantId == entity.ProductVariantId
            );
            Utility.DoesExist(ProductVariant, "Product Variant");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the product variant. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
