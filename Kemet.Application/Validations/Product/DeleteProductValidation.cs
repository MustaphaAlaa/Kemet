using Entities.Models.DTOs;
using IServices.IProductServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// Product Delete Validation
public class DeleteProductValidation : IDeleteProductValidation
{
    private readonly IRetrieveProduct _getProduct;
    private readonly ILogger<DeleteProductValidation> _logger;

    public DeleteProductValidation(IRetrieveProduct getProduct, ILogger<DeleteProductValidation> logger)
    {
        _getProduct = getProduct;
        _logger = logger;
    }

    public async Task Validate(ProductDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ProductId);

            var product = await _getProduct.GetByAsync(p => p.ProductId == entity.ProductId);

            Utility.DoesExist(product, "Product");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the deletion of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
