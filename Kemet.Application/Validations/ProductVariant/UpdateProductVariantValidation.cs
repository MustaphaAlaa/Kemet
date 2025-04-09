using Application.IProductVariantServices;
using Entities.Models.DTOs;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// ProductVariant Update Validation
public class UpdateProductVariantValidation : IUpdateProductVariantValidation
{
    private readonly IRetrieveProductVariant _getProductVariant;
    private readonly ILogger<UpdateProductVariantValidation> _logger;

    public UpdateProductVariantValidation(IRetrieveProductVariant getProductVariant, ILogger<UpdateProductVariantValidation> logger)
    {
        _getProductVariant = getProductVariant;
        _logger = logger;
    }

    public async Task<ProductVariantReadDTO> Validate(ProductVariantUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ProductVariantId);

            var productVariant = await _getProductVariant.GetByAsync(pv => pv.ProductVariantId == entity.ProductVariantId);

            Utility.DoesExist(productVariant, "Product Variant");

            return productVariant;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the update of the product variant. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
