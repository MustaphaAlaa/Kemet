using Application.IProductVariantServices;
using Entities.Models.DTOs;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// ProductVariant Delete Validation
public class DeleteProductVariantValidation : IDeleteProductVariantValidation
{
    private readonly IRetrieveProductVariant _getProductVariant;
    private readonly ILogger<DeleteProductVariantValidation> _logger;

    public DeleteProductVariantValidation(IRetrieveProductVariant getProductVariant, ILogger<DeleteProductVariantValidation> logger)
    {
        _getProductVariant = getProductVariant;
        _logger = logger;
    }

    public async Task Validate(ProductVariantDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ProductVariantId);

            var productVariant = await _getProductVariant.GetByAsync(pv => pv.ProductVariantId == entity.ProductVariantId);

            Utility.DoesExist(productVariant, "Product Variant");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the deletion of the product variant. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
