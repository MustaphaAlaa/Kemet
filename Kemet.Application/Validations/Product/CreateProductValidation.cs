using AutoMapper;
using Entities.Models.DTOs;
using IServices.IProductServices;

using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// Product Validation
public class CreateProductValidation : ICreateProductValidation
{
    private readonly IProductService _getProduct;
    private readonly ILogger<CreateProductValidation> _logger;

    public CreateProductValidation(IProductService getProduct, ILogger<CreateProductValidation> logger)
    {
        _getProduct = getProduct;
        _logger = logger;
    }

    public async Task Validate(ProductCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IsNullOrEmpty(entity.Name, "Product Name");

            entity.Name = entity.Name?.Trim().ToLower();

            var product = await _getProduct.GetByAsync(p => p.Name == entity.Name);

            Utility.AlreadyExist(product, "Product");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the creation of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
