﻿using System.Linq.Expressions;
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
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
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

        Mock<ServiceFacade_DependenceInjection<Size, SizeService>> ServiceFacaseDI =
                new(_unitOfWork.Object, _logger.Object, _helper.Object, _mapper.Object);

        _sizeService = new SizeService(ServiceFacaseDI.Object, _sizeValidation.Object);
    }

    #region Create
    [Fact]
    public async Task CreateAsync_EntityDTOisNul_ThrowsValidationException()
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
    public async Task CreateAsync_PropertyIsNullOrEmpty_ThrowsValidationException(string sizeName)
    {
        var dto = new SizeCreateDTO { Name = sizeName };

        _sizeValidation
            .Setup(x => x.ValidateCreate(It.IsAny<SizeCreateDTO>()))
            .ThrowsAsync(new ValidationException("Property is null"));

        Func<Task> action = async () => await _sizeService.CreateAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        var dto = _fixture.Build<SizeCreateDTO>().Create();

        _sizeValidation
            .Setup(x => x.ValidateCreate(It.IsAny<SizeCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException("Property is null"));

        Func<Task> action = async () => await _sizeService.CreateAsync(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    #endregion


    #region Update
    [Fact]
    public async Task Update_RequestIsNull_ThorwValidationException()
    {
        _sizeValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<SizeUpdateDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _sizeService.Update(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task Update_PropertyIsNullOrEmpty_ThrowsValidationException(string sizeName)
    {
        var dto = new SizeUpdateDTO { Name = sizeName };

        _sizeValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<SizeUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _sizeService.Update(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task Update_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        var dto = _fixture.Build<SizeUpdateDTO>().Create();

        _sizeValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<SizeUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        Func<Task> action = async () => await _sizeService.Update(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    #endregion


    #region Delete

    [Fact]
    public async Task DeleteAsync_EntityDTOisNul_ThrowsValidationException()
    {
        _sizeValidation
            .Setup(x => x.ValidateDelete(It.IsAny<SizeDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _sizeService.DeleteAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsValidationException(int id)
    {
        var dto = new SizeDeleteDTO { SizeId = id };

        _sizeValidation
            .Setup(x => x.ValidateDelete(It.IsAny<SizeDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _sizeService.DeleteAsync(dto);

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
        var expected = await _sizeService.RetrieveByAsync(It.IsAny<Expression<Func<Size, bool>>>());

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
        var expected = await _sizeService.RetrieveByAsync(It.IsAny<Expression<Func<Size, bool>>>());

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
