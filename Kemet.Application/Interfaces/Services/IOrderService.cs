using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderService
    : IServiceAsync<Order, int, OrderCreateDTO, OrderDeleteDTO, OrderUpdateDTO, OrderReadDTO> { }
