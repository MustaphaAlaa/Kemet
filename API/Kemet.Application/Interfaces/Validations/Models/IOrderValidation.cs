using Entities.Models.DTOs;

namespace Entities.Models.Interfaces.Validations;

public interface IOrderValidation : IValidator<OrderCreateDTO, OrderUpdateDTO, OrderDeleteDTO> { }
