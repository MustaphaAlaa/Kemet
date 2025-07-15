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

public class GovernorateValidationTEST
{
    private readonly Mock<IBaseRepository<Governorate>> _repository;
    private readonly Mock<ILogger<GovernorateValidation>> _logger;

    private readonly IValidator<GovernorateCreateDTO> _governorateCreateValidation;
    private readonly IValidator<GovernorateUpdateDTO> _governorateUpdateValidation;
    private readonly IValidator<GovernorateDeleteDTO> _governorateDeleteValidation;
    private readonly IGovernorateValidation _governorateValidation;

    public GovernorateValidationTEST()
    {
        _governorateCreateValidation = new GovernorateCreateValidation();
        _governorateUpdateValidation = new GovernorateUpdateValidation();
        _governorateDeleteValidation = new GovernorateDeleteValidation();

        _repository = new();
        _logger = new();
        _governorateValidation = new GovernorateValidation(
            _repository.Object,
            _logger.Object,
            _governorateCreateValidation,
            _governorateUpdateValidation,
            _governorateDeleteValidation
        );
    }

    #region  Create
    [Fact]
    public async Task CreateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _governorateValidation.ValidateCreate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsync_PropertiesIsInvaild_ThrowsValidationException(string name)
    {
        //Arrange
        var dto = new GovernorateCreateDTO { Name = name };

        //Act
        Func<Task> action = async () => await _governorateValidation.ValidateCreate(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExistException()
    {
        //Arrange
        var dto = new GovernorateCreateDTO { Name = "Governorate Name" };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ReturnsAsync(new Governorate());
        //Act
        Func<Task> action = async () => await _governorateValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }
    #endregion

    #region  Update
    [Fact]
    public async Task UpdateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _governorateValidation.ValidateUpdate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null, -1)]
    [InlineData("", 0)]
    [InlineData("valid", 0)]
    [InlineData("", 5)]
    public async Task UpdateAsync_PropertiesIsInvalid_ThrowsValidationException(
        string name,
        int GovernorateId
    )
    {
        //Arrange
        var dto = new GovernorateUpdateDTO { Name = name, GovernorateId = GovernorateId };

        //Act
        Func<Task> action = async () => await _governorateValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateAsync_DoesNotExist_ThrowsDoesNotExistException()
    {
        //Arrange
        var dto = new GovernorateUpdateDTO { GovernorateId = 134253, Name = "Governorate Name" };

        _repository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ReturnsAsync(null as Governorate);

        //Act
        Func<Task> action = async () => await _governorateValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }
    #endregion

    #region  Delete
    [Fact]
    public async Task DeleteValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act
        Func<Task> action = async () => await _governorateValidation.ValidateDelete(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteAsync_PropertiesIsInvalid_ThrowsValidationException(int GovernorateId)
    {
        //Arrange
        var dto = new GovernorateDeleteDTO { GovernorateId = GovernorateId };

        //Act
        Func<Task> action = async () => await _governorateValidation.ValidateDelete(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion
}
