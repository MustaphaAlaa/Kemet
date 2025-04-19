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



    #region  Create
    private async Task<CustomerReadDTO> CreateCore(CustomerCreateDTO entity)
    {
        await _CustomerValidation.ValidateCreate(entity);

        var customer = _mapper.Map<Customer>(entity);

        customer.CreatedAt = DateTime.Now;

        customer = await _repository.CreateAsync(customer);

        return _mapper.Map<CustomerReadDTO>(customer);
    }

    public async Task<CustomerReadDTO> CreateInternalAsync(CustomerCreateDTO entity)
    {
        try
        {
            var CustomerDto = await this.CreateCore(entity);
            await _unitOfWork.SaveChangesAsync();
            return CustomerDto;
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

    public async Task<CustomerReadDTO> CreateAsync(CustomerCreateDTO entity)
    {
        try
        {
            var customer = await CreateCore(entity);
            return customer;
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
    #endregion






    #region Delete
    private async Task DeleteCore(CustomerDeleteDTO entity)
    {
        await _CustomerValidation.ValidateDelete(entity);
        await _repository.DeleteAsync(g => g.CustomerId == entity.CustomerId);
    }

    public async Task DeleteAsync(CustomerDeleteDTO entity)
    {
        try
        {
            await DeleteCore(entity);
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

    public async Task<bool> DeleteInternalAsync(CustomerDeleteDTO entity)
    {
        try
        {
            await DeleteCore(entity);
            await _repository.DeleteAsync(g => g.CustomerId == entity.CustomerId);
            var isDeleted = await _unitOfWork.SaveChangesAsync() > 0;
            return isDeleted;
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

    #endregion






    #region  Update


    private async Task<CustomerReadDTO> UpdateCore(CustomerUpdateDTO updateRequest)
    {
        await _CustomerValidation.ValidateUpdate(updateRequest);

        var Customer = _mapper.Map<Customer>(updateRequest);

        Customer = _repository.Update(Customer);

        return _mapper.Map<CustomerReadDTO>(Customer);
    }

    public async Task<CustomerReadDTO> UpdateInternalAsync(CustomerUpdateDTO updateRequest)
    {
        try
        {
            var Customer = await this.Update(updateRequest);

            await _unitOfWork.SaveChangesAsync();

            return Customer;
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

    public async Task<CustomerReadDTO> Update(CustomerUpdateDTO updateRequest)
    {
        try
        {
            var customer = await UpdateCore(updateRequest);
            return customer;
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

    #endregion






    #region  Retrieve


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

    #endregion
}
