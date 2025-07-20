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
using Kemet.Application.Interfaces;
using Kemet.Application.Services;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CustomerService
    : GenericService<Customer, CustomerReadDTO, CustomerService>,
        ICustomerService
{
    private readonly IBaseRepository<Customer> _repository;

    private readonly ICustomerValidation _CustomerValidation;

    public CustomerService(
        IServiceFacade_DependenceInjection<Customer, CustomerService> facade,
        ICustomerValidation CustomerValidation
    )
        : base(facade, "Customer")
    {
        _repository = _unitOfWork.GetRepository<Customer>();
        _CustomerValidation = CustomerValidation;
    }

    public async Task<CustomerReadDTO> CreateAsync(CustomerCreateDTO entity)
    {
        try
        {
            await _CustomerValidation.ValidateCreate(entity);

            var customer = _mapper.Map<Customer>(entity);

            if (customer.UserId == null)
            {
                customer.IsAnonymous = true;
            }
            else
                customer.IsAnonymous = false;
            ;

            customer.CreatedAt = DateTime.Now;

            customer = await _repository.CreateAsync(customer);

            return _mapper.Map<CustomerReadDTO>(customer);
        }
        catch (ValidationException ex)
        {
            _logger.LogInformation(ex, $"Validation exception thrown while creating the {TName}.");
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(
                ex,
                $"The user is doesn't exist so {TName} cannot be created fo this user id ."
            );
            throw;
        }
        catch (AlreadyExistException ex)
        {
            _logger.LogInformation(
                ex,
                $"this anonymous {TName} has registered previously with the same phone number"
            );
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An unexpected error occurred while creating the {TName}.");
            throw;
        }
    }

    public async Task DeleteAsync(CustomerDeleteDTO entity)
    {
        try
        {
            await _CustomerValidation.ValidateDelete(entity);

            await _repository.DeleteAsync(g =>
                (g.CustomerId == entity.CustomerId || g.PhoneNumber == entity.PhoneNumber)
            );
        }
        catch (ValidationException ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
            _logger.LogInformation(msg);
            throw;
        }
        catch (Exception ex)
        {
            string msg = $"An error thrown while deleting the {TName}. {ex.Message}";
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
                $"Validating Exception is thrown while updating the {TName}."
            );
            throw;
        }
        catch (DoesNotExistException ex)
        {
            _logger.LogInformation(ex, $"{TName} doesn't exist.");
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error thrown while validating the updating of the {TName}.");
            throw;
        }
    }

    public async Task<CustomerReadDTO> GetById(Guid key)
    {
        return await this.RetrieveByAsync(entity => entity.CustomerId == key);
    }

    public async Task<CustomerReadDTO> FindCustomerByPhoneNumberAsync(string phoneNumber)
    {
        return await this.RetrieveByAsync(cutomer => cutomer.PhoneNumber == phoneNumber);
    }

    public async Task<bool> IsCustomerExist(string phoneNumber)
    {
        var customer = await this.FindCustomerByPhoneNumberAsync(phoneNumber);

        return customer != null;
    }
}
