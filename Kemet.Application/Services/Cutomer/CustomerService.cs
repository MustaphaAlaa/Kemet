using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Domain.IServices;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using FluentValidation;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CustomerService : SaveService, ICustomerService
{
    private readonly IBaseRepository<Customer> _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerService> _logger;
    private readonly ICustomerValidation _CustomerValidation;
    private readonly IRepositoryRetrieverHelper<Customer> _repositoryHelper;

    public CustomerService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        ILogger<CustomerService> logger,
        ICustomerValidation CustomerValidation,
        IRepositoryRetrieverHelper<Customer> repositoryHelper
    )
        : base(unitOfWork)
    {
        _repository = _unitOfWork.GetRepository<Customer>();

        _mapper = mapper;
        _logger = logger;
        _CustomerValidation = CustomerValidation;
        _repositoryHelper = repositoryHelper;
    }

    public async Task<CustomerReadDTO> CreateAsync(CustomerCreateDTO entity)
    {
        try
        {
            await _CustomerValidation.ValidateCreate(entity);

            var customer = _mapper.Map<Customer>(entity);

            customer.CreatedAt = DateTime.Now;

            customer = await _repository.CreateAsync(customer);

            return _mapper.Map<CustomerReadDTO>(customer);
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(ex, "Validation exception thrown while creating the customer.");
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(
                ex,
                "The user is doesn't exist so customer cannot be created fo this user id ."
            );
            throw;
        }
        catch (AlreadyExistException ex)
        {
            _logger.LogInformation(
                ex,
                "this anonymous customer has registered previously with the same phone number"
            );
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred while creating the customer.");
            throw;
        }
    }

    public async Task DeleteAsync(CustomerDeleteDTO entity)
    {
        try
        {
            await _CustomerValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.CustomerId == entity.CustomerId);
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the customer. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the customer. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<CustomerReadDTO> Update(CustomerUpdateDTO updateRequest)
    {
        try
        {
            await _CustomerValidation.ValidateUpdate(updateRequest);

            var Customer = _mapper.Map<Customer>(updateRequest);

            Customer = _repository.Update(Customer);

            return _mapper.Map<CustomerReadDTO>(Customer);
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(
                ex,
                "Validating Exception is thrown while updating the Customer."
            );
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(ex, "Customer doesn't exist.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error thrown while validating the updating of the Customer.");
            throw;
        }
    }

    public async Task<List<CustomerReadDTO>> RetrieveAllAsync()
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<CustomerReadDTO>();
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving customer records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<IEnumerable<CustomerReadDTO>> RetrieveAllAsync(
        Expression<Func<Customer, bool>> predicate
    )
    {
        try
        {
            return await _repositoryHelper.RetrieveAllAsync<CustomerReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving customer records. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<CustomerReadDTO> RetrieveByAsync(Expression<Func<Customer, bool>> predicate)
    {
        try
        {
            return await _repositoryHelper.RetrieveByAsync<CustomerReadDTO>(predicate);
        }
        catch (Exception ex)
        {
            string msg =
                $"Unexpected exception throws while retrieving the customer record. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task<CustomerReadDTO> GetById(int key)
    {
        return await this.RetrieveByAsync(entity => entity.CustomerId == key);
    }
}
