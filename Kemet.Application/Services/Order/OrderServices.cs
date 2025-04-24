using System.Linq.Expressions; 
using Application.Exceptions;
using Application.Services;
using AutoMapper;
using Entities.Enmus;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Services;

public class OrderService : SaveService, IOrderService
{
    private readonly IBaseRepository<Order> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderService> _logger;
    private readonly IOrderValidation _orderValidation;
    private readonly IRepositoryRetrieverHelper<Order> _repositoryHelper;

    public OrderService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<OrderService> logger,
        IOrderValidation orderValidation,
        IRepositoryRetrieverHelper<Order> repositoryHelper
    )
        : base(unitOfWork)
    {
        _repository = _unitOfWork.GetRepository<Order>();

        _mapper = mapper;
        _logger = logger;
        _orderValidation = orderValidation;
        _repositoryHelper = repositoryHelper;
    }

    public async Task<OrderReadDTO> CreateAsync(OrderCreateDTO entity)
    {
        try
        {
            await _orderValidation.ValidateCreate(entity);

            var order = _mapper.Map<Order>(entity);

            order.CreatedAt = DateTime.Now;
            order.OrderStatusId = (int)OrderStatusEnum.Pending;
            order.OrderReceiptStatusId = null;
            order.IsPaid = null;

            order = await _repository.CreateAsync(order);

            return _mapper.Map<OrderReadDTO>(order);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while creating the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the creation of the color. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<OrderReadDTO> Update(OrderUpdateDTO updateRequest)
    {
        try
        {
            await _orderValidation.ValidateUpdate(updateRequest);

            var order = _mapper.Map<Order>(updateRequest);

            order.UpdatedAt = DateTime.Now;

            var updatedOrder = _repository.Update(order);

            return _mapper.Map<OrderReadDTO>(order);
        }
        catch (ValidationException ex)
        {
            string msg = $"Validating Exception is thrown while updating the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (DoesNotExistException ex)
        {
            string msg = $"Order doesn't exist. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg =
                $"An error thrown while validating the updating of the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task DeleteAsync(OrderDeleteDTO entity)
    {
        try
        {
            await _orderValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.OrderId == entity.OrderId);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the order. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
    }

    public async Task<List<OrderReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<OrderReadDTO>();
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving order records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<OrderReadDTO>> RetrieveAllAsync(
        Expression<Func<Order, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<OrderReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving order records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<OrderReadDTO> RetrieveByAsync(Expression<Func<Order, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<OrderReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving the order record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<OrderReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.OrderId == key);
    }
}
