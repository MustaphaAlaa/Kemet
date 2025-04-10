using Entities.Models.DTOs;

namespace Kemet.Application.Interfaces.Validations;

public interface IOrderValidation : IValidator<OrderCreateDTO, OrderUpdateDTO, OrderDeleteDTO> { }
