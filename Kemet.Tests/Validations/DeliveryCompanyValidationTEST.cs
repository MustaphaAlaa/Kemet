using System.Linq.Expressions;
using Application.Exceptions;
using AutoFixture;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Validations;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;
using Moq;

namespace Kemet.Tests.Validations;

public class DeliveryCompanyValidationTEST
{
    private readonly Mock<IBaseRepository<DeliveryCompany>> _mockRepository;
    private readonly Mock<ILogger<DeliveryCompanyValidation>> _logger;

    private readonly IValidator<DeliveryCompanyCreateDTO> _deliveryCompanyCreateValidation;
    private readonly IValidator<DeliveryCompanyUpdateDTO> _deliveryCompanyUpdateValidation;
    private readonly IValidator<DeliveryCompanyDeleteDTO> _deliveryCompanyDeleteValidation;
    private readonly IDeliveryCompanyValidation _deliveryCompanyValidation;
    private IFixture _fixture;

    public DeliveryCompanyValidationTEST()
    {
        _deliveryCompanyCreateValidation = new DeliveryCompanyCreateValidation();
        _deliveryCompanyUpdateValidation = new DeliveryCompanyUpdateValidation();
        _deliveryCompanyDeleteValidation = new DeliveryCompanyDeleteValidation();
        _fixture = new Fixture();
        _mockRepository = new();
        _logger = new();
        _deliveryCompanyValidation = new DeliveryCompanyValidation(
            _mockRepository.Object,
            _logger.Object,
            _deliveryCompanyCreateValidation,
            _deliveryCompanyUpdateValidation,
            _deliveryCompanyDeleteValidation
        );
    }

    #region  Create
    [Fact]
    public async Task CreateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateCreate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task CreateAsync_PropertiesIsInvalid_ThrowsValidationException(string name)
    {
        //Arrange
        var dto = new DeliveryCompanyCreateDTO { Name = name };

        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task CreateAsync_AlreadyExist_ThrowsAlreadyExistException()
    {
        //Arrange
        var dto = new DeliveryCompanyCreateDTO { Name = "new Company" };

        _mockRepository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<DeliveryCompany, bool>>>()))
            .ReturnsAsync(new DeliveryCompany());

        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateCreate(dto);

        await Assert.ThrowsAsync<AlreadyExistException>(async () => await action());
    }
    #endregion

    #region  Update
    [Fact]
    public async Task UpdateValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act

        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateUpdate(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(null, 1)]
    [InlineData("", 1)]
    [InlineData("valid", -1)]
    [InlineData("valid", 0)]
    [InlineData("", -1)]
    public async Task UpdateAsync_PropertiesIsInvalid_ThrowsValidationException(
        string name,
        int deliveryCompanyId
    )
    {
        //Arrange
        var dto = new DeliveryCompanyUpdateDTO
        {
            DeliveryCompanyId = deliveryCompanyId,
            Name = name,
        };

        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task UpdateAsync_DoesNotExist_ThrowsDoesNotExistException()
    {
        //Arrange
        var dto = new DeliveryCompanyUpdateDTO
        {
            DeliveryCompanyId = 134253,

            Name = "DeliveryCompany Name",
        };

        _mockRepository
            .Setup(x => x.RetrieveAsync(It.IsAny<Expression<Func<DeliveryCompany, bool>>>()))
            .ReturnsAsync(null as DeliveryCompany);

        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateUpdate(dto);

        await Assert.ThrowsAsync<DoesNotExistException>(async () => await action());
    }
    #endregion

    #region  Delete
    [Fact]
    public async Task DeleteValidation_EntityIsNull_ThrowsArgumentNullException()
    {
        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateDelete(null);

        //Assert
        await Assert.ThrowsAsync<ArgumentNullException>(() => action());
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public async Task DeleteAsync_PropertiesIsInvalid_ThrowsValidationException(int colorId)
    {
        //Arrange
        var dto = new DeliveryCompanyDeleteDTO { DeliveryCompanyId = colorId };

        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateDelete(dto);

        await Assert.ThrowsAsync<ValidationException>(async () => await action());
    }

    [Fact]
    public async Task DeleteAsync_DeliveryCompanyHasOrders_ThrowsException()
    {
        //Arrange
        var dto = new DeliveryCompanyDeleteDTO { DeliveryCompanyId = 1 };

        ICollection<Order> Orders = _fixture
            .Build<Order>()
            .With(o => o.DeliveryCompany, null as DeliveryCompany)
            .With(o => o.OrderItems, null as ICollection<OrderItem>)
            .CreateMany()
            .ToList();

        var dc = _fixture.Build<DeliveryCompany>().With(c => c.Orders, Orders).Create();

        _mockRepository
            .Setup(x =>
                x.RetrieveWithIncludeAsync(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>(),
                    It.IsAny<Expression<Func<DeliveryCompany, object>>>()
                )
            )
            .ReturnsAsync(dc);

        //Act
        Func<Task> action = async () => await _deliveryCompanyValidation.ValidateDelete(dto);

        await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    [Fact]
    public async Task DeleteAsync_DeliveryCompanyDoesNotHaveOrders_ThrowsValidationException()
    {
        //Arrange
        var dto = new DeliveryCompanyDeleteDTO { DeliveryCompanyId = 1 };

        ICollection<Order> Orders = new List<Order>();

        var dc = _fixture.Build<DeliveryCompany>().With(c => c.Orders, Orders).Create();

        _mockRepository
            .Setup(x =>
                x.RetrieveWithIncludeAsync(
                    It.IsAny<Expression<Func<DeliveryCompany, bool>>>(),
                    It.IsAny<Expression<Func<DeliveryCompany, object>>>()
                )
            )
            .ReturnsAsync(dc);

        //Act
        await _deliveryCompanyValidation.ValidateDelete(dto);

        // await Assert.ThrowsAsync<Exception>(async () => await action());
    }

    #endregion
}
