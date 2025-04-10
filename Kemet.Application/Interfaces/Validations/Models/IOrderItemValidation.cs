using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IOrderItemValidation
    : IValidator<OrderItemCreateDTO, OrderItemUpdateDTO, OrderItemDeleteDTO> { }
