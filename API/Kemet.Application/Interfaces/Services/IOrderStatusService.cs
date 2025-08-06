using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;

namespace IServices;

public interface IOrderStatusService
    : IServiceAsync<OrderStatus, int, OrderStatus, OrderStatus, OrderStatus, OrderStatusReadDTO>
{
    Task<OrderStatusReadDTO> GetById(int key);
    }
