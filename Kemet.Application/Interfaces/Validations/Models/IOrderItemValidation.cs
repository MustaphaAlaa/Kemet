using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IOrderItemValidation
    : IValidator<OrderItemCreateDTO, OrderItemUpdateDTO, OrderItemDeleteDTO> { }
