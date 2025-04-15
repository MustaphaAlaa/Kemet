using System.Linq.Expressions;
using Application.Exceptions;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Helpers;
using Entities.Models.Interfaces.Validations;
using IRepository.Generic;
using IServices;
using Microsoft.Extensions.Logging;

namespace Application;
 

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
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
    {
        _unitOfWork = unitOfWork;
        _repository = _unitOfWork.GetRepository<Customer>();

        _mapper = mapper;
        _logger = logger;
        _CustomerValidation = CustomerValidation;
        _repositoryHelper = repositoryHelper;
    }

    public async Task<CustomerReadDTO> CreateInternalAsync(CustomerCreateDTO entity)
    {
        try
        {
            var CustomerDto = await this.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return CustomerDto;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while creating the Customer. \n{ex.Message}";
            _logger.LogError(msg);
            throw new FailedToCreateException(msg);
            throw;
        }
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
        catch (FailedToCreateException ex) // Or potentially catch more specific DB exceptions first
        {
            // Log the original exception correctly
            _logger.LogError(ex, "A known creation failure occurred while creating the customer.");
            // Re-throw the original exception to preserve the stack trace and type
            throw;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            string errorMsg = "An unexpected error occurred while creating the customer.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            throw new FailedToCreateException(errorMsg, ex);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(CustomerDeleteDTO entity)
    {
        try
        {
            await _CustomerValidation.ValidateDelete(entity);
            await _repository.DeleteAsync(g => g.CustomerId == entity.CustomerId);
            var isDeleted = await _unitOfWork.SaveChangesAsync() > 0;
            return isDeleted;
        }
        catch (Exception ex)
        {
            var msg = $"An error occurred while deleting the Customer.  {ex.Message}";
            _logger.LogError(msg);
            throw new FailedToDeleteException(msg);
            throw;
        }
    }

    public async Task<List<CustomerReadDTO>> RetrieveAllAsync()
    {
        return await _repositoryHelper.RetrieveAllAsync<CustomerReadDTO>();
    }

    public async Task<IEnumerable<CustomerReadDTO>> RetrieveAllAsync(
        Expression<Func<Customer, bool>> predicate
    )
    {
        return await _repositoryHelper.RetrieveAllAsync<CustomerReadDTO>(predicate);
    }

    public async Task<CustomerReadDTO> RetrieveByAsync(Expression<Func<Customer, bool>> predicate)
    {
        return await _repositoryHelper.RetrieveByAsync<CustomerReadDTO>(predicate);
    }

    public async Task<CustomerReadDTO> UpdateInternalAsync(CustomerUpdateDTO updateRequest)
    {
        try
        {
            var Customer = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return Customer;
        }
        catch (FailedToUpdateException ex) // Or potentially catch more specific DB exceptions first
        {
            // Log the original exception correctly
            _logger.LogError(ex, "A known creation failure occurred while updating the customer.");
            // Re-throw the original exception to preserve the stack trace and type
            throw;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            string errorMsg = "An unexpected error occurred while updating the customer.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            throw new FailedToUpdateException(errorMsg, ex);
            throw;
        }
    }

    public async Task<CustomerReadDTO> Update(CustomerUpdateDTO updateRequest)
    {
        try
        {
            await _CustomerValidation.ValidateUpdate(updateRequest);

            var Customer = _mapper.Map<Customer>(updateRequest);

            var updatedCustomer = _repository.Update(Customer);

            return _mapper.Map<CustomerReadDTO>(Customer);
        }
        catch (FailedToUpdateException ex) // Or potentially catch more specific DB exceptions first
        {
            // Log the original exception correctly
            _logger.LogError(ex, "A known creation failure occurred while updating the customer.");
            // Re-throw the original exception to preserve the stack trace and type
            throw;
        }
        catch (Exception ex) // Catch any other unexpected exceptions
        {
            string errorMsg = "An unexpected error occurred while updating the customer.";
            // Log the original exception correctly
            _logger.LogError(ex, errorMsg);
            // Wrap the original exception in your custom type
            throw new FailedToUpdateException(errorMsg, ex);
            throw;
        }
    }
}
