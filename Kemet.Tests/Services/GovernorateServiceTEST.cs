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

public class GovernorateServiceTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly IGovernorateService _governorateService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IBaseRepository<Governorate>> _mockRepository;
    private readonly Mock<IGovernorateValidation> _governorateValidation;
    private readonly Mock<IRepositoryRetrieverHelper<Governorate>> _helper;

    public GovernorateServiceTEST()
    {
        _fixture = new Fixture();

        _mapper = new Mock<IMapper>();

        _mockRepository = new Mock<IBaseRepository<Governorate>>();

        _unitOfWork = new Mock<IUnitOfWork>();

        Mock<ILogger<GovernorateService>> _logger = new();
        _helper = new();

        _governorateValidation = new();
        _unitOfWork.Setup(uow => uow.GetRepository<Governorate>()).Returns(_mockRepository.Object);

        _governorateService = new GovernorateService(
            _unitOfWork.Object,
            _mapper.Object,
            _logger.Object,
            _governorateValidation.Object,
            _helper.Object
        );
    }

    #region Create
    [Fact]
    public async Task CreateAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _governorateValidation
            .Setup(x => x.ValidateCreate(It.IsAny<GovernorateCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _governorateService.CreateAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsync_PropertyIsNullOrEmpty_ThrowsValidationException(string GovernorateName)
    {
        var dto = new GovernorateCreateDTO { Name = GovernorateName };

        _governorateValidation
            .Setup(x => x.ValidateCreate(It.IsAny<GovernorateCreateDTO>()))
            .ThrowsAsync(new ValidationException("Property is null"));

        Func<Task> action = async () => await _governorateService.CreateAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        var dto = _fixture.Build<GovernorateCreateDTO>().Create();

        _governorateValidation
            .Setup(x => x.ValidateCreate(It.IsAny<GovernorateCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException("Property is null"));

        Func<Task> action = async () => await _governorateService.CreateAsync(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _governorateValidation
            .Setup(x => x.ValidateCreate(It.IsAny<GovernorateCreateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _governorateService.CreateInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateInternalAsync_PropertyIsNullOrEmpty_ThrowsValidationException(
        string GovernorateName
    )
    {
        var dto = new GovernorateCreateDTO { Name = GovernorateName };

        _governorateValidation
            .Setup(x => x.ValidateCreate(It.IsAny<GovernorateCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _governorateService.CreateInternalAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        var dto = _fixture.Build<GovernorateCreateDTO>().Create();

        _governorateValidation
            .Setup(x => x.ValidateCreate(It.IsAny<GovernorateCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException("Property is null"));

        Func<Task> action = async () => await _governorateService.CreateInternalAsync(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    #endregion


    #region Update
    [Fact]
    public async Task Update_RequestIsNull_ThorwValidationException()
    {
        _governorateValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<GovernorateUpdateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _governorateService.UpdateInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Update_PropertyIsNullOrEmpty_ThrowsValidationException(string GovernorateName)
    {
        var dto = new GovernorateUpdateDTO { Name = GovernorateName };

        _governorateValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<GovernorateUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _governorateService.Update(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task Update_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        var dto = _fixture.Build<GovernorateUpdateDTO>().Create();

        _governorateValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<GovernorateUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        Func<Task> action = async () => await _governorateService.Update(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateInternalAsync_PropertyIsNullOrEmpty_ThrowsValidationException(
        string GovernorateName
    )
    {
        var dto = new GovernorateUpdateDTO { Name = GovernorateName };

        _governorateValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<GovernorateUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _governorateService.UpdateInternalAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateInternal_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        var dto = _fixture.Build<GovernorateUpdateDTO>().Create();

        _governorateValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<GovernorateUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        Func<Task> action = async () => await _governorateService.UpdateInternalAsync(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    #endregion


    #region Delete

    [Fact]
    public async Task DeleteAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _governorateValidation
            .Setup(x => x.ValidateDelete(It.IsAny<GovernorateDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _governorateService.DeleteInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteInternalAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _governorateValidation
            .Setup(x => x.ValidateDelete(It.IsAny<GovernorateDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _governorateService.DeleteInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsValidationException(int id)
    {
        var dto = new GovernorateDeleteDTO { GovernorateId = id };

        _governorateValidation
            .Setup(x => x.ValidateDelete(It.IsAny<GovernorateDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _governorateService.DeleteAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteInternalAsync_PropertyIsNullOrEmpty_ThrowsValidationException(int id)
    {
        //Arrange
        var dto = new GovernorateDeleteDTO { GovernorateId = id };

        _governorateValidation
            .Setup(x => x.ValidateDelete(It.IsAny<GovernorateDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Act
        Func<Task> action = async () => await _governorateService.DeleteInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion


    #region Retrieves

    [Fact]
    public async Task RetrieveAllAsync_ReturnsGovernorateReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<GovernorateReadDTO>>().Create();

        _helper.Setup(x => x.RetrieveAllAsync<GovernorateReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _governorateService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_EmptyList_ReturnsEmptyGovernorateReadDTOList()
    {
        //Arrange
        var ls = new List<GovernorateReadDTO>();

        _helper.Setup(x => x.RetrieveAllAsync<GovernorateReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _governorateService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Empty(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange
        _helper.Setup(x => x.RetrieveAllAsync<GovernorateReadDTO>()).ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () => await _governorateService.RetrieveAllAsync();

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_ReturnsGovernorateReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<GovernorateReadDTO>>().Create();

        _helper
            .Setup(x => x.RetrieveAllAsync<GovernorateReadDTO>(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _governorateService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Governorate, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_EmptyList_ReturnsEmptyGovernorateReadDTOList()
    {
        //Arrange
        var ls = new List<GovernorateReadDTO>();

        _helper
            .Setup(x => x.RetrieveAllAsync<GovernorateReadDTO>(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _governorateService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Governorate, bool>>>()
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
            .Setup(x => x.RetrieveAllAsync<GovernorateReadDTO>(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _governorateService.RetrieveAllAsync(It.IsAny<Expression<Func<Governorate, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveByAsync_FoundTheGovernorate_ReturnsGovernorateReadDTO()
    {
        //Arrange
        var dto = _fixture.Build<GovernorateReadDTO>().Create();

        _helper
            .Setup(x => x.RetrieveByAsync<GovernorateReadDTO>(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _governorateService.RetrieveByAsync(It.IsAny<Expression<Func<Governorate, bool>>>());

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_NotFound_ReturnsEmptyGovernorateReadDTO()
    {
        //Arrange
        var dto = new GovernorateReadDTO();

        _helper
            .Setup(x => x.RetrieveByAsync<GovernorateReadDTO>(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _governorateService.RetrieveByAsync(It.IsAny<Expression<Func<Governorate, bool>>>());

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange

        _helper
            .Setup(x => x.RetrieveByAsync<GovernorateReadDTO>(It.IsAny<Expression<Func<Governorate, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _governorateService.RetrieveByAsync(It.IsAny<Expression<Func<Governorate, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    #endregion
}
