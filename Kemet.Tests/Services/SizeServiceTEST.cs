using System.Linq.Expressions;
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

public class SizeServiceTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly ISizeService _sizeService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IBaseRepository<Size>> _mockRepository;
    private readonly Mock<ISizeValidation> _sizeValidation;
    private readonly Mock<IRepositoryRetrieverHelper<Size>> _helper;

    public SizeServiceTEST()
    {
        _fixture = new Fixture();

        _mapper = new Mock<IMapper>();

        _mockRepository = new Mock<IBaseRepository<Size>>();

        _unitOfWork = new Mock<IUnitOfWork>();

        Mock<ILogger<SizeService>> _logger = new();
        _helper = new();

        _sizeValidation = new();
        _unitOfWork.Setup(uow => uow.GetRepository<Size>()).Returns(_mockRepository.Object);

        _sizeService = new SizeService(
            _unitOfWork.Object,
            _sizeValidation.Object,
            _mapper.Object,
            _logger.Object,
            _helper.Object
        );
    }

    #region Create
    [Fact]
    public async Task CreateAsync_EntityDTOisNul_ThrowsArgumentNullException()
    {
        _sizeValidation
            .Setup(x => x.ValidateCreate(It.IsAny<SizeCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _sizeService.CreateAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(
        string sizeName
    )
    {
        var dto = new SizeCreateDTO { Name = sizeName };

        _sizeValidation
            .Setup(x => x.ValidateCreate(It.IsAny<SizeCreateDTO>()))
            .ThrowsAsync(new ValidationException("Property is null"));

        Func<Task> action = async () => await _sizeService.CreateAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateInternalAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        _sizeValidation
            .Setup(x => x.ValidateCreate(It.IsAny<SizeCreateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _sizeService.CreateInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(
        string sizeName
    )
    {
        var dto = new SizeCreateDTO { Name = sizeName };

        _sizeValidation
            .Setup(x => x.ValidateCreate(It.IsAny<SizeCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _sizeService.CreateInternalAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }
    #endregion


    #region Update
    [Fact]
    public async Task UpdateAsync_RequestIsNull_ThorwArgumentNullException()
    {
        _sizeValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<SizeUpdateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _sizeService.UpdateInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(
        string sizeName
    )
    {
        var dto = new SizeUpdateDTO { Name = sizeName };

        _sizeValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<SizeUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _sizeService.Update(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task UpdateInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(
        string sizeName
    )
    {
        var dto = new SizeUpdateDTO { Name = sizeName };

        _sizeValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<SizeUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _sizeService.UpdateInternalAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }
    #endregion


    #region Delete

    [Fact]
    public async Task DeleteAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        _sizeValidation
            .Setup(x => x.ValidateDelete(It.IsAny<SizeDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _sizeService.DeleteInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteInternalAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        _sizeValidation
            .Setup(x => x.ValidateDelete(It.IsAny<SizeDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _sizeService.DeleteInternalAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
    {
        var dto = new SizeDeleteDTO { SizeId = id };

        _sizeValidation
            .Setup(x => x.ValidateDelete(It.IsAny<SizeDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _sizeService.DeleteAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteInternalAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
    {
        //Arrange
        var dto = new SizeDeleteDTO { SizeId = id };

        _sizeValidation
            .Setup(x => x.ValidateDelete(It.IsAny<SizeDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        //Act
        Func<Task> action = async () => await _sizeService.DeleteInternalAsync(dto);

        //Assert
        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion


    #region Retrieves

    [Fact]
    public async Task RetrieveAllAsync_ReturnsSizeReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<SizeReadDTO>>().Create();

        _helper.Setup(x => x.RetrieveAllAsync<SizeReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _sizeService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_EmptyList_ReturnsEmptySizeReadDTOList()
    {
        //Arrange
        var ls = new List<SizeReadDTO>();

        _helper.Setup(x => x.RetrieveAllAsync<SizeReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _sizeService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Empty(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange
        _helper.Setup(x => x.RetrieveAllAsync<SizeReadDTO>()).ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () => await _sizeService.RetrieveAllAsync();

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_ReturnsSizeReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<SizeReadDTO>>().Create();

        _helper
            .Setup(x => x.RetrieveAllAsync<SizeReadDTO>(It.IsAny<Expression<Func<Size, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _sizeService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Size, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_EmptyList_ReturnsEmptySizeReadDTOList()
    {
        //Arrange
        var ls = new List<SizeReadDTO>();

        _helper
            .Setup(x => x.RetrieveAllAsync<SizeReadDTO>(It.IsAny<Expression<Func<Size, bool>>>()))
            .ReturnsAsync(ls);

        //Act
        var expected = await _sizeService.RetrieveAllAsync(
            It.IsAny<Expression<Func<Size, bool>>>()
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
            .Setup(x => x.RetrieveAllAsync<SizeReadDTO>(It.IsAny<Expression<Func<Size, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _sizeService.RetrieveAllAsync(It.IsAny<Expression<Func<Size, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }



    [Fact]
    public async Task RetrieveByAsync_FoundTheSize_ReturnsSizeReadDTO()
    {
        //Arrange
        var dto = _fixture.Build<SizeReadDTO>().Create();

        _helper
            .Setup(x => x.RetrieveByAsync<SizeReadDTO>(It.IsAny<Expression<Func<Size, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _sizeService.RetrieveByAsync(
            It.IsAny<Expression<Func<Size, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_NotFound_ReturnsEmptySizeReadDTO()
    {
        //Arrange
        var dto = new SizeReadDTO();

        _helper
            .Setup(x => x.RetrieveByAsync<SizeReadDTO>(It.IsAny<Expression<Func<Size, bool>>>()))
            .ReturnsAsync(dto);

        //Act
        var expected = await _sizeService.RetrieveByAsync(
            It.IsAny<Expression<Func<Size, bool>>>()
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
            .Setup(x => x.RetrieveByAsync<SizeReadDTO>(It.IsAny<Expression<Func<Size, bool>>>()))
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _sizeService.RetrieveByAsync(It.IsAny<Expression<Func<Size, bool>>>());

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    #endregion
}
