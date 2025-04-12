using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using IRepository.Generic;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

public class ProductCreateValidation : AbstractValidator<ProductCreateDTO>
{
    public ProductCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.CategoryId).LessThan(0).WithMessage("Category Id is required.");
    }
}

public class ProductUpdateValidation : AbstractValidator<ProductUpdateDTO>
{
    public ProductUpdateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");
        RuleFor(x => x.ProductId).LessThan(1).WithMessage("Product ID must be greater than 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.CategoryId).LessThan(0).WithMessage("Category Id is required.");
    }
}

public class ProductDeleteValidation : AbstractValidator<ProductDeleteDTO>
{
    public ProductDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.ProductId).LessThan(1).WithMessage("Product ID must be greater than 0.");
    }
}

public class CreateProductValidation : IProductValidation
{
    private readonly IBaseRepository<Product> _repository;
    private readonly ILogger<CreateProductValidation> _logger;
    private readonly IValidator<ProductCreateDTO> _productCreateValidation;
    private readonly IValidator<ProductUpdateDTO> _productUpdateValidation;
    private readonly IValidator<ProductDeleteDTO> _productDeleteValidation;

    public async Task ValidateCreate(ProductCreateDTO entity)
    {
        try
        {
            await _productCreateValidation.ValidateAndThrowAsync(entity);

            entity.Name = entity.Name?.Trim().ToLower();

            var product = await _repository.RetrieveAsync(p => p.Name == entity.Name);

            Utility.AlreadyExist(product, "Product");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(ProductDeleteDTO entity)
    {
        try
        {
            await _productDeleteValidation.ValidateAndThrowAsync(entity);

            var product = await _repository.RetrieveAsync(p => p.ProductId == entity.ProductId);

            Utility.DoesExist(product, "Product");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the product. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public Task ValidateUpdate(ProductUpdateDTO entity)
    {
        throw new NotImplementedException();
    }
}
