using System.Linq.Expressions;
using Application.Exceptions;
using Application.Services;
using AutoFixture;
using AutoMapper;
using Castle.Core.Logging;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;
using Moq;

namespace Kemet.Tests.Services;

public class ColorServiceTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly IColorService _colorService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IBaseRepository<Color>> _mockRepository;
    private readonly Mock<IColorValidation> _colorValidation;
    private readonly Mock<IRepositoryRetrieverHelper<Color>> _helper;

    public ColorServiceTEST()
    {
        _fixture = new Fixture();

        _mapper = new Mock<IMapper>();

        _mockRepository = new Mock<IBaseRepository<Color>>();

        _unitOfWork = new Mock<IUnitOfWork>();

        Mock<ILogger<ColorService>> _logger = new();
        _helper = new();

        _colorValidation = new();
        _unitOfWork.Setup(uow => uow.GetRepository<Color>()).Returns(_mockRepository.Object);

        _colorService = new ColorService(
            _colorValidation.Object,
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
        _colorValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ColorCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _colorService.CreateAsync(null);

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
    public async Task CreateAsync_PropertyIsNullOrEmpty_ThrowsValidationException(
        string colorName,
        string hexacode
    )
    {
        var dto = new ColorCreateDTO { HexaCode = hexacode, Name = colorName };

        _colorValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ColorCreateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _colorService.CreateAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _colorValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ColorCreateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _colorService.CreateInternalAsync(null);

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
        var dto = new ColorCreateDTO { HexaCode = hexacode, Name = colorName };

        _colorValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ColorCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _colorService.CreateInternalAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        var dto = _fixture.Build<ColorCreateDTO>().Create();

        _colorValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ColorCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException(""));

        Func<Task> action = async () => await _colorService.CreateAsync(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        var dto = _fixture.Build<ColorCreateDTO>().Create();

        _colorValidation
            .Setup(x => x.ValidateCreate(It.IsAny<ColorCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException("Property is null"));

        Func<Task> action = async () => await _colorService.CreateInternalAsync(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    #endregion


    #region Update
    [Fact]
    public async Task UpdateAsync_RequestIsNull_ThorwArgumentNullException()
    {
        _colorValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ColorUpdateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _colorService.UpdateInternalAsync(null);

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
        var dto = new ColorUpdateDTO { HexaCode = hexacode, Name = colorName };

        _colorValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ColorUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _colorService.Update(dto);

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
        var dto = new ColorUpdateDTO { HexaCode = hexacode, Name = colorName };

        _colorValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ColorUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _colorService.UpdateInternalAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task Update_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        var dto = _fixture.Build<ColorUpdateDTO>().Create();

        _colorValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ColorUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        Func<Task> action = async () => await _colorService.Update(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    [Fact]
    public async Task UpdateInternalAsync_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        var dto = _fixture.Build<ColorUpdateDTO>().Create();

        _colorValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<ColorUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        Func<Task> action = async () => await _colorService.UpdateInternalAsync(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    #endregion


    #region Delete

    [Fact]
    public async Task DeleteAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        _colorValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ColorDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _colorService.DeleteInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteInternalAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        _colorValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ColorDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _colorService.DeleteInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
    {
        var dto = new ColorDeleteDTO { ColorId = id };

        _colorValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ColorDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _colorService.DeleteAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
    {
        //Arrange
        var dto = new ColorDeleteDTO { ColorId = id };

        _colorValidation
            .Setup(x => x.ValidateDelete(It.IsAny<ColorDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Act
        Func<Task> action = async () => await _colorService.DeleteInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion


    #region Retrieves

    [Fact]
    public async Task RetrieveAllAsync_ReturnsColorReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<ColorReadDTO>>().Create();

        _helper.Setup(x => x.RetrieveAllAsync<ColorReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _colorService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_EmptyList_ReturnsEmptyColorReadDTOList()
    {
        //Arrange
        var ls = new List<ColorReadDTO>();

        _helper.Setup(x => x.RetrieveAllAsync<ColorReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _colorService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Empty(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange
        _helper.Setup(x => x.RetrieveAllAsync<ColorReadDTO>()).ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () => await _colorService.RetrieveAllAsync();

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_ReturnsColorReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<ColorReadDTO>>().Create();

        _helper
            .Setup(x => x.RetrieveAllAsync<ColorReadDTO>(It.IsAny<Expression<Func<Color, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _colorService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Color, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_EmptyList_ReturnsEmptyColorReadDTOList()
    {
        //Arrange
        var ls = new List<ColorReadDTO>();

        _helper
            .Setup(x => x.RetrieveAllAsync<ColorReadDTO>(It.IsAny<Expression<Func<Color, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _colorService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Color, bool>>>()
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
            .Setup(x => x.RetrieveAllAsync<ColorReadDTO>(It.IsAny<Expression<Func<Color, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _colorService.RetrieveAllAsync(It.IsAny<Expression<Func<Color, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveByAsync_FoundTheColor_ReturnsColorReadDTO()
    {
        //Arrange
        var dto = _fixture.Build<ColorReadDTO>().Create();

        _helper
            .Setup(x => x.RetrieveByAsync<ColorReadDTO>(It.IsAny<Expression<Func<Color, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _colorService.RetrieveByAsync(
            It.IsAny<Expression<Func<Color, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_NotFound_ReturnsEmptyColorReadDTO()
    {
        //Arrange
        var dto = new ColorReadDTO();

        _helper
            .Setup(x => x.RetrieveByAsync<ColorReadDTO>(It.IsAny<Expression<Func<Color, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _colorService.RetrieveByAsync(
            It.IsAny<Expression<Func<Color, bool>>>()
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
            .Setup(x => x.RetrieveByAsync<ColorReadDTO>(It.IsAny<Expression<Func<Color, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _colorService.RetrieveByAsync(It.IsAny<Expression<Func<Color, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }
    #endregion
}
