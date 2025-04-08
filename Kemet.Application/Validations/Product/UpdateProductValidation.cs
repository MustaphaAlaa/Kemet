using AutoMapper;
using Entities.Models.DTOs;
using IServices.IProductServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

public class UpdateProductValidation : IUpdateProductValidation
{
    private readonly IRetrieveProduct _getProduct;
    private readonly ILogger<UpdateProductValidation> _logger;

    public UpdateProductValidation(IRetrieveProduct getProduct, ILogger<UpdateProductValidation> logger)
    {
        _getProduct = getProduct;
        _logger = logger;
    }

    public async Task<ProductReadDTO> Validate(ProductUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ProductId);
            Utility.IsNullOrEmpty(entity.Name, "Product Name");

            entity.Name = entity.Name?.Trim().ToLower();

            var product = await _getProduct.GetByAsync(p => p.ProductId == entity.ProductId);

            Utility.DoesExist(product, "Product");

            return product;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the update of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
