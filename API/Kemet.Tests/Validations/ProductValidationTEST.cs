using System.Linq.Expressions;
using Application.Exceptions;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Validations;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;
using Moq;

namespace Kemet.Tests.Validations;

public class ProductValidationTEST
{
    private readonly Mock<IBaseRepository<Product>> _repository;
    private readonly Mock<ILogger<ProductValidation>> _logger;

    private readonly IValidator<ProductCreateDTO> _productCreateValidation;
    private readonly IValidator<ProductUpdateDTO> _productUpdateValidation;
    private readonly IValidator<ProductDeleteDTO> _productDeleteValidation;
    private readonly IProductValidation _productValidation;

    public ProductValidationTEST()
    {
        _productCreateValidation = new ProductCreateValidation();
        _productUpdateValidation = new ProductUpdateValidation();
        _productDeleteValidation = new ProductDeleteValidation();

        _repository = new();
        _logger = new();
        _productValidation = new ProductValidation(
            _repository.Object,
            _logger.Object,
            _productCreateValidation,
            _productUpdateValidation,
            _productDeleteValidation
        );
    }

    #region  Create
    [Fact]
    public async Task CreateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _productValidation.ValidateCreate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null, "", -1)]
    [InlineData("", null, 0)]
    [InlineData("", "valid", 0)]
    [InlineData("valid", null, -1)]
    [InlineData(null, "valid", 1)]
    public async Task CreateAsync_PropertiesIsInvaild_ThrowsValidationException(
        string name,
        string description,
        int categoryId
    )
    {
        //Arrange
        var dto = new ProductCreateDTO
        {
            Description = description,
            Name = name,
            CategoryId = categoryId,
        };

        //Act
        Func<Task> action = async () => await _productValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExistException()
    {
        //Arrange
        var dto = new ProductCreateDTO
        {
            Description = "this is description",
            Name = "Product Name",
            CategoryId = 178,
        };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(new Product());
        //Act
        Func<Task> action = async () => await _productValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }
    #endregion

    #region  Update
    [Fact]
    public async Task UpdateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _productValidation.ValidateUpdate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null, "", -1)]
    [InlineData("", null, 0)]
    [InlineData("", "valid", 0)]
    [InlineData("valid", null, -1)]
    [InlineData(null, "valid", 1)]
    public async Task UpdateAsync_PropertiesIsInvaild_ThrowsValidationException(
        string name,
        string description,
        int categoryId
    )
    {
        //Arrange
        var dto = new ProductUpdateDTO
        {
            Description = description,
            Name = name,
            CategoryId = categoryId,
        };

        //Act
        Func<Task> action = async () => await _productValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateAsync_DoesNotExist_ThrowsDoesNotExistException()
    {
        //Arrange
        var dto = new ProductUpdateDTO
        {
            ProductId = 134253,
            Description = "this is description",
            Name = "Product Name",
            CategoryId = 178,
        };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(null as Product);

        //Act
        Func<Task> action = async () => await _productValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }
    #endregion

    #region  Delete
    [Fact]
    public async Task DeleteValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act
        Func<Task> action = async () => await _productValidation.ValidateDelete(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteAsync_PropertiesIsInvalid_ThrowsValidationException(int productId)
    {
        //Arrange
        var dto = new ProductDeleteDTO { ProductId = productId };

        //Act
        Func<Task> action = async () => await _productValidation.ValidateDelete(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion
}
