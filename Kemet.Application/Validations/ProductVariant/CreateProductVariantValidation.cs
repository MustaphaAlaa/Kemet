using Application.IProductVariantServices;
using Entities.Models.DTOs;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// ProductVariant Validation
public class CreateProductVariantValidation : ICreateProductVariantValidation
{
    private readonly IRetrieveProductVariant _getProductVariant;
    private readonly ILogger<CreateProductVariantValidation> _logger;

    public CreateProductVariantValidation(IRetrieveProductVariant getProductVariant, ILogger<CreateProductVariantValidation> logger)
    {
        _getProductVariant = getProductVariant;
        _logger = logger;
    }

    public async Task Validate(ProductVariantCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IsNullOrEmpty(entity.SKU, "Product Variant SKU");

            entity.SKU = entity.SKU?.Trim().ToLower();

            var productVariant = await _getProductVariant.GetByAsync(pv => pv.SKU == entity.SKU);

            Utility.AlreadyExist(productVariant, "Product Variant");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the creation of the product variant. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
