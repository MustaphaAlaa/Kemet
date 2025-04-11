using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderService
    : IServiceAsync<Order, OrderCreateDTO, OrderDeleteDTO, OrderUpdateDTO, OrderReadDTO> { }
