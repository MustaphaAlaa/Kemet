using System.Linq.Expressions;
using Application.Exceptions;
using Application.Services;
using AutoFixture;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;
using Moq;

namespace Kemet.Tests.Services;

public class ProductServiceTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly IProductService _productService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IBaseRepository<Product>> _mockRepository;
    private readonly Mock<IProductValidation> _productValidation;
    private readonly Mock<IRepositoryRetrieverHelper<Product>> _helper;

    public ProductServiceTEST()
    {
        _fixture = new Fixture();

        _mapper = new Mock<IMapper>();

        _mockRepository = new Mock<IBaseRepository<Product>>();

        _unitOfWork = new Mock<IUnitOfWork>();

        Mock<ILogger<ProductService>> _logger = new();
        _helper = new();

        _productValidation = new();
        _unitOfWork.Setup(uow => uow.GetRepository<Product>()).Returns(_mockRepository.Object);

        _productService = new ProductService(
            _productValidation.Object,
            _unitOfWork.Object,
            _mapper.Object,
            _logger.Object,
            _helper.Object
        );
    }

    #region Create
    [Fact]
    public async Task CreateAsync_EntityDTOisNul_ThrowsValidationException()
    {
        //Arrange
        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        //Act
        Func<Task> action = async () => await _productService.CreateAsync(null);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }


    [Fact]
    public async Task CreateAsync_PropertiesIsInvalid_ThrowsValidationException()
    {
        //Arrang 
        var dto = _fixture.Build<ProductCreateDTO>()
        .With(p => p.Name, "")
        .With(p => p.Description, null as string)
        .With(p => p.CategoryId, 0)
        .Create();

        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
                 .ThrowsAsync(new ValidationException("Properties are null"));
        //Act
        Func<Task> action = async () => await _productService.CreateAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_EntityDTOisNul_ThrowsValidationException()
    {
        //Arrange
        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        //Act
        Func<Task> action = async () => await _productService.CreateInternalAsync(null);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_PropertyIsNullOrEmpty_ThrowsValidationException()
    {
        //Arrange
        var dto = _fixture.Build<ProductCreateDTO>().Create();

        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        //Act
        Func<Task> action = async () => await _productService.CreateInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        //Arrange
        var dto = _fixture.Build<ProductCreateDTO>().Create();

        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException(""));

        //Act
        Func<Task> action = async () => await _productService.CreateAsync(dto);

        //Asert
        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        //Arrange
        var dto = _fixture.Build<ProductCreateDTO>().Create();

        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException("Property is null"));

        //Act
        Func<Task> action = async () => await _productService.CreateInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    #endregion


    #region Update
    [Fact]
    public async Task UpdateAsync_RequestIsNull_ThorwArgumentNullException()
    {
        //Arrange
        _productValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        //Act
        Func<Task> action = async () => await _productService.UpdateInternalAsync(null);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]

    public async Task UpdateAsync_PropertyIsNullOrEmpty_ThrowsArgumentException()
    {
        //Arrange
        var dto = _fixture.Build<ProductUpdateDTO>()
            .With(p => p.Name, "")
            .With(p => p.Description, null as string)
            .With(p => p.CategoryId, 0)
            .Create();

        _productValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Act
        Func<Task> action = async () => await _productService.Update(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException()
    {
        //Arrange 
        var dto = _fixture.Build<ProductUpdateDTO>()
        .With(p => p.Name, "")
        .With(p => p.Description, null as string)
        .With(p => p.CategoryId, 0)
        .Create();

        _productValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Act
        Func<Task> action = async () => await _productService.UpdateInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task Update_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        //Arrange
        var dto = _fixture.Build<ProductUpdateDTO>().Create();

        _productValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        //Act
        Func<Task> action = async () => await _productService.Update(dto);

        //Assert
        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    [Fact]
    public async Task UpdateInternalAsync_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        //Arrange
        var dto = _fixture.Build<ProductUpdateDTO>().Create();

        _productValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        //Act
        Func<Task> action = async () => await _productService.UpdateInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    #endregion


    #region Delete

    [Fact]
    public async Task DeleteAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        //Arrange
        _productValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        //Act
        Func<Task> action = async () => await _productService.DeleteInternalAsync(null);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteInternalAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        //Arrange
        _productValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        //Act
        Func<Task> action = async () => await _productService.DeleteInternalAsync(null);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsArgumentException()
    {
        //Arrange
        var dto = new ProductDeleteDTO { ProductId = 0 };

        _productValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Assert
        Func<Task> action = async () => await _productService.DeleteAsync(dto);

        //Act
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException()
    {
        //Arrange
        var dto = new ProductDeleteDTO { ProductId = 0 };

        _productValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Act
        Func<Task> action = async () => await _productService.DeleteInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion


    #region Retrieves

    [Fact]
    public async Task RetrieveAllAsync_ReturnsProductReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<ProductReadDTO>>().Create();

        _helper.Setup(x => x.RetrieveAllAsync<ProductReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _productService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_EmptyList_ReturnsEmptyProductReadDTOList()
    {
        //Arrange
        var ls = new List<ProductReadDTO>();

        _helper.Setup(x => x.RetrieveAllAsync<ProductReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _productService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Empty(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange
        _helper.Setup(x => x.RetrieveAllAsync<ProductReadDTO>()).ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () => await _productService.RetrieveAllAsync();

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_ReturnsProductReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<ProductReadDTO>>().Create();

        _helper
            .Setup(x => x.RetrieveAllAsync<ProductReadDTO>(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _productService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Product, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsyncWithExpression_EmptyList_ReturnsEmptyProductReadDTOList()
    {
        //Arrange
        var ls = new List<ProductReadDTO>();

        _helper
            .Setup(x => x.RetrieveAllAsync<ProductReadDTO>(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _productService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Product, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Empty(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsyncWithExpression_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange

        _helper
            .Setup(x => x.RetrieveAllAsync<ProductReadDTO>(It.IsAny<Expression<Func<Product, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _productService.RetrieveAllAsync(It.IsAny<Expression<Func<Product, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveByAsync_FoundTheProduct_ReturnsProductReadDTO()
    {
        //Arrange
        var dto = _fixture.Build<ProductReadDTO>().Create();

        _helper
            .Setup(x => x.RetrieveByAsync<ProductReadDTO>(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _productService.RetrieveByAsync(
            It.IsAny<Expression<Func<Product, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_NotFound_ReturnsEmptyProductReadDTO()
    {
        //Arrange
        var dto = new ProductReadDTO();

        _helper
            .Setup(x => x.RetrieveByAsync<ProductReadDTO>(It.IsAny<Expression<Func<Product, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _productService.RetrieveByAsync(
            It.IsAny<Expression<Func<Product, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange

        _helper
            .Setup(x => x.RetrieveByAsync<ProductReadDTO>(It.IsAny<Expression<Func<Product, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _productService.RetrieveByAsync(It.IsAny<Expression<Func<Product, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }
    #endregion

}
