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
using IRepository;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace Kemet.Tests.Services;

public class DeliveryCompanyServiceTEST
{
    private readonly IFixture _fixture;
    private readonly Mock<IMapper> _mapper;

    private readonly IDeliveryCompanyService _deliveryCompanyService;
    private readonly Mock<IUnitOfWork> _unitOfWork;
    private readonly Mock<IDeliveryCompanyRepository > _mockRepository;
    private readonly Mock<IDeliveryCompanyValidation> _deliveryCompanyValidation;
    private readonly Mock<IRepositoryRetrieverHelper<DeliveryCompany>> _helper;

    public DeliveryCompanyServiceTEST()
    {
        _fixture = new Fixture();

        _mapper = new Mock<IMapper>();

        _mockRepository = new Mock<IDeliveryCompanyRepository>();

        _unitOfWork = new Mock<IUnitOfWork>();

        Mock<ILogger<DeliveryCompanyService>> _logger = new();
        _helper = new();

        _deliveryCompanyValidation = new();
        _unitOfWork
            .Setup(uow => uow.GetRepository<DeliveryCompany>())
            .Returns(_mockRepository.Object);

        Mock<
            ServiceFacade_DependenceInjection<DeliveryCompany, DeliveryCompanyService>
        > ServiceFacadeDI = new(_unitOfWork.Object, _logger.Object, _helper.Object, _mapper.Object);

        _deliveryCompanyService = new DeliveryCompanyService(
            ServiceFacadeDI.Object,
            _deliveryCompanyValidation.Object,
            _mockRepository.Object
        );
    }

    #region Create

    [Fact]
    public async Task CreateAsync_EntityDTOisNull_ThrowsValidationException()
    {
        _deliveryCompanyValidation
            .Setup(x => x.ValidateCreate(It.IsAny<DeliveryCompanyCreateDTO>()))
            .ThrowsAsync(new ValidationException(""));

        Func<Task> action = async () => await _deliveryCompanyService.CreateAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_PropertyIsNullOrEmpty_ThrowsValidationException()
    {
        var dto = new DeliveryCompanyCreateDTO { Name = "" };

        _deliveryCompanyValidation
            .Setup(x => x.ValidateCreate(It.IsAny<DeliveryCompanyCreateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _deliveryCompanyService.CreateAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExist()
    {
        var dto = _fixture.Build<DeliveryCompanyCreateDTO>().Create();

        _deliveryCompanyValidation
            .Setup(x => x.ValidateCreate(It.IsAny<DeliveryCompanyCreateDTO>()))
            .ThrowsAsync(new AlreadyExistException(""));

        Func<Task> action = async () => await _deliveryCompanyService.CreateAsync(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_successCreation_returnDeliveryCompanyReadDTO()
    {
        //arrange
        var dto = _fixture.Build<DeliveryCompanyCreateDTO>().Create();


        /*var expectedDeliveryCompany =
            _fixture.Build<DeliveryCompany>()
                .With(dc => dc.Orders, null as ICollection<Order>)
                .Create();*/

        var readDto = _fixture.Build<DeliveryCompanyReadDTO>() 
            .With(dc => dc.Orders, null as ICollection<Order>)
            .With(dc => dc.GovernorateDeliveryCompanies, null as ICollection<GovernorateDeliveryCompany>)
            .Create();


        _mockRepository.Setup(x => x.CreateAsync(It.IsAny<DeliveryCompany>()))
            .ReturnsAsync(new DeliveryCompany());
        
        _mapper.Setup(x => x.Map<DeliveryCompanyReadDTO>(It.IsAny<DeliveryCompany>())).Returns(readDto);

        //act
        var actualDC = await _deliveryCompanyService.CreateAsync(dto);

        //assert
        Assert.Equivalent(readDto, actualDC);
    }

       

    #endregion


    #region Update

    [Fact]
    public async Task UpdateAsync_PropertyIsNullOrEmpty_ThrowsArgumentException()
    {
        var dto = new DeliveryCompanyUpdateDTO { };

        _deliveryCompanyValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<DeliveryCompanyUpdateDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _deliveryCompanyService.Update(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task Update_DoesNotExist_ThrowsDoesNotExistExceptino()
    {
        var dto = _fixture.Build<DeliveryCompanyUpdateDTO>().Create();

        _deliveryCompanyValidation
            .Setup(x => x.ValidateUpdate(It.IsAny<DeliveryCompanyUpdateDTO>()))
            .ThrowsAsync(new DoesNotExistException());

        Func<Task> action = async () => await _deliveryCompanyService.Update(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }

    #endregion


    #region Delete

    [Fact]
    public async Task DeleteAsync_EntityDTOisNul_ThrowsArgumentException()
    {
        _deliveryCompanyValidation
            .Setup(x => x.ValidateDelete(It.IsAny<DeliveryCompanyDeleteDTO>()))
            .ThrowsAsync(new ValidationException("validation faliure"));

        Func<Task> action = async () => await _deliveryCompanyService.DeleteAsync(null);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public async Task DeleteAsync_PropertyIsNullOrEmpty_ThrowsArgumentException(int id)
    {
        var dto = new DeliveryCompanyDeleteDTO { DeliveryCompanyId = id };

        _deliveryCompanyValidation
            .Setup(x => x.ValidateDelete(It.IsAny<DeliveryCompanyDeleteDTO>()))
            .ThrowsAsync(new ValidationException("Properties are null"));

        Func<Task> action = async () => await _deliveryCompanyService.DeleteAsync(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    #endregion


    #region Retrieves

    [Fact]
    public async Task RetrieveAllAsync_ReturnsDeliveryCompanyReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<DeliveryCompanyReadDTO>>().Create();

        _helper.Setup(x => x.RetrieveAllAsync<DeliveryCompanyReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _deliveryCompanyService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_EmptyList_ReturnsEmptyDeliveryCompanyReadDTOList()
    {
        //Arrange
        var ls = new List<DeliveryCompanyReadDTO>();

        _helper.Setup(x => x.RetrieveAllAsync<DeliveryCompanyReadDTO>()).ReturnsAsync(ls);

        //Act
        var expected = await _deliveryCompanyService.RetrieveAllAsync();

        //Assert
        Assert.NotNull(expected);
        Assert.Empty(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_FailureWhileRetrievingTheData_ExceptionThrown()
    {
        //Arrange
        _helper
            .Setup(x => x.RetrieveAllAsync<DeliveryCompanyReadDTO>())
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () => await _deliveryCompanyService.RetrieveAllAsync();

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_ReturnsDeliveryCompanyReadDTOList()
    {
        //Arrange
        var ls = _fixture.Build<List<DeliveryCompanyReadDTO>>().Create();

        _helper
            .Setup(x =>
                x.RetrieveAllAsync<DeliveryCompanyReadDTO>(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
                )
            )
            .ReturnsAsync(ls);

        //Act
        var expected = await _deliveryCompanyService.RetrieveAllAsync(
            It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, ls);
    }

    [Fact]
    public async Task RetrieveAllAsync_WithExpression_EmptyList_ReturnsEmptyDeliveryCompanyReadDTOList()
    {
        //Arrange
        var ls = new List<DeliveryCompanyReadDTO>();

        _helper
            .Setup(x =>
                x.RetrieveAllAsync<DeliveryCompanyReadDTO>(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
                )
            )
            .ReturnsAsync(ls);

        //Act
        var expected = await _deliveryCompanyService.RetrieveAllAsync(
            It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
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
            .Setup(x =>
                x.RetrieveAllAsync<DeliveryCompanyReadDTO>(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
                )
            )
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _deliveryCompanyService.RetrieveAllAsync(
                It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
            );

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task RetrieveByAsync_FoundTheDeliveryCompany_ReturnsDeliveryCompanyReadDTO()
    {
        //Arrange
        var dto = _fixture.Build<DeliveryCompanyReadDTO>().Create();

        _helper
            .Setup(x =>
                x.RetrieveByAsync<DeliveryCompanyReadDTO>(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
                )
            )
            .ReturnsAsync(dto);

        //Act
        var expected = await _deliveryCompanyService.RetrieveByAsync(
            It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
        );

        //Assert
        Assert.NotNull(expected);
        Assert.Equivalent(expected, dto);
    }

    [Fact]
    public async Task RetrieveByAsync_NotFound_ReturnsEmptyDeliveryCompanyReadDTO()
    {
        //Arrange
        var dto = new DeliveryCompanyReadDTO();

        _helper
            .Setup(x =>
                x.RetrieveByAsync<DeliveryCompanyReadDTO>(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
                )
            )
            .ReturnsAsync(dto);

        //Act
        var expected = await _deliveryCompanyService.RetrieveByAsync(
            It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
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
            .Setup(x =>
                x.RetrieveByAsync<DeliveryCompanyReadDTO>(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
                )
            )
            .ThrowsAsync(new Exception());

        //Act
        Func<Task> action = async () =>
            await _deliveryCompanyService.RetrieveByAsync(
                It.IsAny<Expression<Func<DeliveryCompany, bool>>>()
            );

        //Assert
        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    #endregion
}