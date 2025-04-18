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

public class ColorValidationTEST
{
    private readonly Mock<IBaseRepository<Color>> _repository;
    private readonly Mock<ILogger<ColorValidation>> _logger;

    private readonly IValidator<ColorCreateDTO> _colorCreateValidation;
    private readonly IValidator<ColorUpdateDTO> _colorUpdateValidation;
    private readonly IValidator<ColorDeleteDTO> _colorDeleteValidation;
    private readonly IColorValidation _colorValidation;

    public ColorValidationTEST()
    {
        _colorCreateValidation = new ColorCreateValidation();
        _colorUpdateValidation = new ColorUpdateValidation();
        _colorDeleteValidation = new ColorDeleteValidation();

        _repository = new();
        _logger = new();
        _colorValidation = new ColorValidation(
            _logger.Object,
            _repository.Object,
            _colorCreateValidation,
            _colorUpdateValidation,
            _colorDeleteValidation
        );
    }

    #region  Create
    [Fact]
    public async Task CreateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _colorValidation.ValidateCreate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData("", null)]
    [InlineData(null, "valid")]
    [InlineData("valid", null)]
    [InlineData("", "")]
    [InlineData("", "valid")]
    public async Task CreateAsync_PropertiesIsInvaild_ThrowsValidationException(
        string name,
        string description
    )
    {
        //Arrange
        var dto = new ColorCreateDTO { HexaCode = description, Name = name };

        //Act
        Func<Task> action = async () => await _colorValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExistException()
    {
        //Arrange
        var dto = new ColorCreateDTO { HexaCode = "#0000", Name = "Color Name" };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Color, bool>>>()))
            .ReturnsAsync(new Color());
        //Act
        Func<Task> action = async () => await _colorValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }
    #endregion

    #region  Update
    [Fact]
    public async Task UpdateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _colorValidation.ValidateUpdate(null);

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
        string Hexacode,
        int colorId
    )
    {
        //Arrange
        var dto = new ColorUpdateDTO
        {
            HexaCode = Hexacode,
            Name = name,
            ColorId = colorId,
        };

        //Act
        Func<Task> action = async () => await _colorValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateAsync_DoesNotExist_ThrowsDoesNotExistException()
    {
        //Arrange
        var dto = new ColorUpdateDTO
        {
            ColorId = 134253,
            HexaCode = "this is hexacode",
            Name = "Color Name",
        };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Color, bool>>>()))
            .ReturnsAsync(null as Color);

        //Act
        Func<Task> action = async () => await _colorValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }
    #endregion

    #region  Delete
    [Fact]
    public async Task DeleteValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act
        Func<Task> action = async () => await _colorValidation.ValidateDelete(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteAsync_PropertiesIsInvalid_ThrowsValidationException(int colorId)
    {
        //Arrange
        var dto = new ColorDeleteDTO { ColorId = colorId };

        //Act
        Func<Task> action = async () => await _colorValidation.ValidateDelete(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion
}
