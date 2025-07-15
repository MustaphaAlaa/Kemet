using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class ProductValidation : IProductValidation
{
    private readonly IBaseRepository<Product> _repository;
    private readonly ILogger<ProductValidation> _logger;
    private readonly IValidator<ProductCreateDTO> _productCreateValidation;
    private readonly IValidator<ProductUpdateDTO> _productUpdateValidation;
    private readonly IValidator<ProductDeleteDTO> _productDeleteValidation;

    public ProductValidation(
        IBaseRepository<Product> repository,
        ILogger<ProductValidation> logger,
        IValidator<ProductCreateDTO> productCreateValidation,
        IValidator<ProductUpdateDTO> productUpdateValidation,
        IValidator<ProductDeleteDTO> productDeleteValidation
    )
    {
        _repository = repository;
        _logger = logger;
        _productCreateValidation = productCreateValidation;
        _productUpdateValidation = productUpdateValidation;
        _productDeleteValidation = productDeleteValidation;
    }

    public async Task ValidateCreate(ProductCreateDTO entity)
    {
        Utility.IsNull(entity);

        var validator = await _productCreateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        entity.Name = entity.Name?.Trim().ToLower();

        var product = await _repository.RetrieveAsync(p => p.Name == entity.Name);

        Utility.AlreadyExist(product, "Product");
    }

    public async Task ValidateDelete(ProductDeleteDTO entity)
    {
        var validator = await _productDeleteValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

    }

    public async Task ValidateUpdate(ProductUpdateDTO entity)
    {
        var validator = await _productUpdateValidation.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        entity.Name = entity.Name?.Trim().ToLower();

        var product = await _repository.RetrieveAsync(p => p.ProductId == entity.ProductId);

        Utility.DoesExist(product, "Product");
    }
}
