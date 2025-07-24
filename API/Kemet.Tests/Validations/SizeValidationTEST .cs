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

public class SizeValidationTEST
{
    private readonly Mock<IBaseRepository<Size>> _repository;
    private readonly Mock<ILogger<SizeValidation>> _logger;

    private readonly IValidator<SizeCreateDTO> _sizeCreateValidation;
    private readonly IValidator<SizeUpdateDTO> _sizeUpdateValidation;
    private readonly IValidator<SizeDeleteDTO> _sizeDeleteValidation;
    private readonly ISizeValidation _sizeValidation;

    public SizeValidationTEST()
    {
        _sizeCreateValidation = new SizeCreateValidation();
        _sizeUpdateValidation = new SizeUpdateValidation();
        _sizeDeleteValidation = new SizeDeleteValidation();

        _repository = new();
        _logger = new();
        _sizeValidation = new SizeValidation(
            _repository.Object,
            _logger.Object,
            _sizeCreateValidation,
            _sizeUpdateValidation,
            _sizeDeleteValidation
        );
    }

    #region  Create
    [Fact]
    public async Task CreateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _sizeValidation.ValidateCreate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsync_PropertiesIsInvaild_ThrowsValidationException(string name)
    {
        //Arrange
        var dto = new SizeCreateDTO { Name = name };

        //Act
        Func<Task> action = async () => await _sizeValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExistException()
    {
        //Arrange
        var dto = new SizeCreateDTO { Name = "Size Name" };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Size, bool>>>()))
            .ReturnsAsync(new Size());
        //Act
        Func<Task> action = async () => await _sizeValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }
    #endregion

    #region  Update
    [Fact]
    public async Task UpdateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _sizeValidation.ValidateUpdate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null, -1)]
    [InlineData("", 0)]
    [InlineData("valid", 0)]
    [InlineData("", 5)]
    public async Task UpdateAsync_PropertiesIsInvaild_ThrowsValidationException(
        string name,
        int sizeId
    )
    {
        //Arrange
        var dto = new SizeUpdateDTO { Name = name, SizeId = sizeId };

        //Act
        Func<Task> action = async () => await _sizeValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateAsync_DoesNotExist_ThrowsDoesNotExistException()
    {
        //Arrange
        var dto = new SizeUpdateDTO { SizeId = 134253, Name = "Size Name" };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Size, bool>>>()))
            .ReturnsAsync(null as Size);

        //Act
        Func<Task> action = async () => await _sizeValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }
    #endregion

    #region  Delete
    [Fact]
    public async Task DeleteValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act
        Func<Task> action = async () => await _sizeValidation.ValidateDelete(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteAsync_PropertiesIsInvalid_ThrowsValidationException(int sizeId)
    {
        //Arrange
        var dto = new SizeDeleteDTO { SizeId = sizeId };

        //Act
        Func<Task> action = async () => await _sizeValidation.ValidateDelete(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion
}
