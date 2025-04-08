using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices.IProductServices;
using Kemet.Application.Interfaces.Validations;
using Microsoft.Extensions.Logging;

namespace Application.ProductServices;


public class CreateProductService : ICreateProduct
{
    private readonly ICreateProductValidation _createProductValidation;
    private readonly ICreateAsync<Product> _createProduct;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateProductService> _logger;

    public CreateProductService(ICreateProductValidation createProductValidation,
        ICreateAsync<Product> createProduct,
        IMapper mapper,
        ILogger<CreateProductService> logger)
    {
        _createProductValidation = createProductValidation;
        _createProduct = createProduct;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO entity)
    {
        try
        {
            await _createProductValidation.Validate(entity);

            var product = _mapper.Map<Product>(entity);
            product = await _createProduct.CreateAsync(product);

            var newProduct = _mapper.Map<ProductReadDTO>(product);

            return newProduct;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the product. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
    }
}
