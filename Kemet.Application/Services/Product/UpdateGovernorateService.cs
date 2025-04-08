using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IProductServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.ProductServices;

public class UpdateProductService : IUpdateProduct
{
    private readonly IUpdateProductValidation _updateProductValidation;
    private readonly IUpdateAsync<Product> _updateProduct;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateProductService> _logger;

    public UpdateProductService(IUpdateProductValidation updateProductValidation,
        IUpdateAsync<Product> updateProduct,
        IMapper mapper,
        ILogger<UpdateProductService> logger)
    {
        _updateProductValidation = updateProductValidation;
        _updateProduct = updateProduct;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductReadDTO> UpdateAsync(ProductUpdateDTO entity)
    {
        try
        {
            var existingProduct = await _updateProductValidation.Validate(entity);
            
            existingProduct.Name = entity.Name;
            existingProduct.Price = entity.Price;
            existingProduct.Description = existingProduct.Description;      
            existingProduct.CategoryId = entity.CategoryId;

            existingProduct.UpdatedAt = DateTime.Now;

            var productToUpdate = _mapper.Map<Product>(existingProduct);

            var updatedProduct = await _updateProduct.UpdateAsync(productToUpdate);

            existingProduct = _mapper.Map<ProductReadDTO>(updatedProduct);

            return existingProduct;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while updating the product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToUpdateException(msg);
            throw;
        }
    }
}
