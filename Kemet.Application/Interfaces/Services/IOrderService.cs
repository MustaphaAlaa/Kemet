using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderService
    : IServiceAsync<Order, OrderDeleteDTO, OrderUpdateDTO, OrderReadDTO> { }
