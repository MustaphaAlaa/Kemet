using System.Linq.Expressions;
using Application.Services;
using Entities.Models;
using Entities.Models.DTOs;
using IRepository.Generic;
using IServices;
using Kemet.Application.Interfaces;

namespace Kemet.Application.Services;

public class OrderReceiptStatusService
    : GenericService<OrderReceiptStatus, OrderReceiptStatusReadDTO, OrderReceiptStatusService>,
        IOrderReceiptStatusService
{
    private readonly IBaseRepository<OrderReceiptStatus> _repository;
 

    public OrderReceiptStatusService(
        IServiceFacade_DependenceInjection<OrderReceiptStatus, OrderReceiptStatusService> facade,
        IBaseRepository<OrderReceiptStatus> repository
    )
        : base(facade, "OrderReceiptStatus")
    {
        _repository = repository;
    }

    public Task<OrderReceiptStatusReadDTO> CreateAsync(OrderReceiptStatus entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(OrderReceiptStatus entity)
    {
        throw new NotImplementedException();
    }

    public async Task<OrderReceiptStatusReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.OrderReceiptStatusId == key);
    }

    public async Task<IEnumerable<OrderReceiptStatusReadDTO>> RetrieveAllAsync(
        Expression<Func<Order, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<OrderReceiptStatusReadDTO>();
    }

    public async Task<OrderReceiptStatusReadDTO> RetrieveByAsync(
        Expression<Func<OrderReceiptStatus, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveByAsync<OrderReceiptStatusReadDTO>(predicate);
    }

    public Task<OrderReceiptStatusReadDTO> Update(OrderReceiptStatus updateRequest)
    {
        throw new NotImplementedException();
    }
};
