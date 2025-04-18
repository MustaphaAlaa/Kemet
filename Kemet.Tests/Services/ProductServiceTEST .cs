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

    //#region Create
    [Fact]
    public async Task CreateAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _productService.CreateAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null, null, 0)]
    [InlineData(null, null, -1)]
    [InlineData(null, null, 1)]
    [InlineData(null, "Navy", 0)]
    [InlineData(null, "Navy", -1)]
    [InlineData(null, "Navy", 1)]
    [InlineData(null, "", 0)]
    [InlineData(null, "", -1)]
    [InlineData(null, "", 1)]
    [InlineData("", "", 0)]
    [InlineData("", "", -1)]
    [InlineData("", "", 1)]
    [InlineData("", "Navy", 0)]
    [InlineData("", "Navy", -1)]
    [InlineData("", "Navy", 1)]
    [InlineData("", null, 0)]
    [InlineData("", null, 5)]
    [InlineData("", null, -1)]
    [InlineData("Navy", null, 0)]
    [InlineData("Navy", null, -1)]
    [InlineData("Navy", null, 1)]
    [InlineData("Navy", "", 0)]
    [InlineData("Navy", "", -1)]
    [InlineData("Navy", "color", 0)]
    [InlineData("Navy", "color", -1)]
    [InlineData("Navy", "color", 1)]

    public async Task CreateAsync_PropertiesIsNullOrEmpty_ThrowsValidationException(
        string name,
        string description,
        int categoryId
    )
    {
        var dto = new ProductCreateDTO { Description = description, Name = name, CategoryId = categoryId };

        _productValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _productService.CreateAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }
    /*
        [Fact]
        public async Task CreateInternalAsync_EntityDTOisNul_ThrowsValidationException()
        {
            _productValidation
                .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
                .ThrowsAsync(new ValidationException("validation faliure"));

            Func<Task> action = async () => await _productService.CreateInternalAsync(null);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "#00000")]
        [InlineData("Navy", null)]
        [InlineData("", "#00000")]
        [InlineData("Navy", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        public async Task CreateInternalAsync_PropertyIsNullOrEmpty_ThrowsValidationException(
            string colorName,
            string hexacode
        )
        {
            var dto = new ProductCreateDTO { HexaCode = hexacode, Name = colorName };

            _productValidation
                .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
                .ThrowsAsync(new ValidationException(""));

            Func<Task> action = async () => await _productService.CreateInternalAsync(dto);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Fact]
        public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExist()
        {
            var dto = _fixture.Build<ProductCreateDTO>().Create();

            _productValidation
                .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
                .ThrowsAsync(new AlreadyExistException(""));

            Func<Task> action = async () => await _productService.CreateAsync(dto);

            await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
        }

        [Fact]
        public async Task CreateInternalAsync_AlreadyExist_ThrowsAlreadyExist()
        {
            var dto = _fixture.Build<ProductCreateDTO>().Create();

            _productValidation
                .Setup(x => x.ValidateCreate(It.IsAny<ProductCreateDTO>()))
                .ThrowsAsync(new AlreadyExistException("Property is null"));

            Func<Task> action = async () => await _productService.CreateInternalAsync(dto);

            await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
        }

        #endregion


        #region Update
        [Fact]
        public async Task UpdateAsync_RequestIsNull_ThorwArgumentNullException()
        {
            _productValidation
                .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
                .ThrowsAsync(new ValidationException("validation faliure"));

            Func<Task> action = async () => await _productService.UpdateInternalAsync(null);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "#00000")]
        [InlineData("Navy", null)]
        [InlineData("", "#00000")]
        [InlineData("Navy", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        public async Task UpdateAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(
            string colorName,
            string hexacode
        )
        {
            var dto = new ProductUpdateDTO { HexaCode = hexacode, Name = colorName };

            _productValidation
                .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
                .ThrowsAsync(new ValidationException("Properties are null"));

            Func<Task> action = async () => await _productService.Update(dto);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData(null, "#00000")]
        [InlineData("Navy", null)]
        [InlineData("", "#00000")]
        [InlineData("Navy", "")]
        [InlineData(null, "")]
        [InlineData("", null)]
        public async Task UpdateInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(
            string colorName,
            string hexacode
        )
        {
            var dto = new ProductUpdateDTO { HexaCode = hexacode, Name = colorName };

            _productValidation
                .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
                .ThrowsAsync(new ValidationException("Properties are null"));

            Func<Task> action = async () => await _productService.UpdateInternalAsync(dto);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Fact]
        public async Task Update_DoesNotExist_ThrowsDoesNotExistExceptino()
        {
            var dto = _fixture.Build<ProductUpdateDTO>().Create();

            _productValidation
                .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
                .ThrowsAsync(new DoesNotExistException());

            Func<Task> action = async () => await _productService.Update(dto);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
        }

        [Fact]
        public async Task UpdateInternalAsync_DoesNotExist_ThrowsDoesNotExistExceptino()
        {
            var dto = _fixture.Build<ProductUpdateDTO>().Create();

            _productValidation
                .Setup(x => x.ValidateUpdate(It.IsAny<ProductUpdateDTO>()))
                .ThrowsAsync(new DoesNotExistException());

            Func<Task> action = async () => await _productService.UpdateInternalAsync(dto);

            await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
        }

        #endregion


        #region Delete

        [Fact]
        public async Task DeleteAsync_EntityDTOisNul_ThrowsArgumentException()
        {
            _productValidation
                .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
                .ThrowsAsync(new ValidationException("validation faliure"));

            Func<Task> action = async () => await _productService.DeleteInternalAsync(null);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Fact]
        public async Task DeleteInternalAsync_EntityDTOisNul_ThrowsArgumentException()
        {
            _productValidation
                .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
                .ThrowsAsync(new ValidationException("validation faliure"));

            Func<Task> action = async () => await _productService.DeleteInternalAsync(null);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
        {
            var dto = new ProductDeleteDTO { ProductId = id };

            _productValidation
                .Setup(x => x.ValidateDelete(It.IsAny<ProductDeleteDTO>()))
                .ThrowsAsync(new ValidationException("Properties are null"));

            Func<Task> action = async () => await _productService.DeleteAsync(dto);

            await Assert.ThrowsAsync<ValidationException>(async () => await action());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task DeleteInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
        {
            //Arrange
            var dto = new ProductDeleteDTO { ProductId = id };

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
        public async Task RetrieveAllAsync_WithExpression_EmptyList_ReturnsEmptyProductReadDTOList()
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
        */
}
