using System.Linq.Expressions;
using Application.Services;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;

namespace Kemet.Application.Services;

public class OrderStatusService
    : GenericService<OrderStatus, OrderStatusReadDTO, OrderStatusService>,
        IOrderStatusService
{
    private readonly IBaseRepository<OrderStatus> _repository;

    // private readonly IBaseRepository<OrderStatus> _orderStatusRepository;

    public OrderStatusService(
        IServiceFacade_DependenceInjection<OrderStatus, OrderStatusService> facade,
        IBaseRepository<OrderStatus> repository
    )
        : base(facade, "OrderStatus")
    {
        _repository = repository;
    }

    public Task<OrderStatusReadDTO> CreateAsync(OrderStatus entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OrderStatus entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderStatusReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.OrderStatusId == key);
    }

    public async Task<IEnumerable<OrderStatusReadDTO>> RetrieveAllAsync(
        Expression<Func<Order, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<OrderStatusReadDTO>();
    }

    public async Task<OrderStatusReadDTO> RetrieveByAsync(
        Expression<Func<OrderStatus, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveByAsync<OrderStatusReadDTO>(predicate);
    }

    public Task<OrderStatusReadDTO> Update(OrderStatus updateRequest)
    {
        throw new NotImplementedException();
    }
};
