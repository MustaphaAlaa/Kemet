using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IProductServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.ProductServices;

public class DeleteProductService : IDeleteProduct
{
    private readonly IDeleteProductValidation _deleteProductValidation;
    private readonly IDeleteAsync<Product> _deleteProduct;
    private readonly ILogger<DeleteProductService> _logger;

    public DeleteProductService(IDeleteProductValidation deleteProductValidation,
        IDeleteAsync<Product> deleteProduct,
        ILogger<DeleteProductService> logger)
    {
        _deleteProductValidation = deleteProductValidation;
        _deleteProduct = deleteProduct;
        _logger = logger;
    }

    public async Task<bool> DeleteAsync(ProductDeleteDTO entity)
    {
        try
        {
            await _deleteProductValidation.Validate(entity);
            bool isDeleted = await _deleteProduct.DeleteAsync(p => p.ProductId == entity.ProductId) > 0;
            return isDeleted;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the product.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }
}
