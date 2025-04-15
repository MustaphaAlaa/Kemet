using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class CustomerValidation : ICustomerValidation
{
    private readonly IBaseRepository<Customer> _repository;
    private readonly IBaseRepository<User> _userRepository;

    private readonly ILogger<CustomerValidation> _logger;
    private readonly IValidator<CustomerCreateDTO> _createValidator;
    private readonly IValidator<CustomerUpdateDTO> _updateValidator;
    private readonly IValidator<CustomerDeleteDTO> _deleteValidator;

    public CustomerValidation(
        IBaseRepository<Customer> repository,
        IBaseRepository<User> userRepository,
        ILogger<CustomerValidation> logger,
        IValidator<CustomerCreateDTO> createValidator,
        IValidator<CustomerUpdateDTO> updateValidator,
        IValidator<CustomerDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _userRepository = userRepository;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(CustomerCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            if (entity.UserId > 0)
            {
                var user = await _userRepository.RetrieveAsync(u => u.UserId == entity.UserId);
                Utility.DoesExist(user);
            }
            else
            {
                var customer = await _repository.RetrieveAsync(c =>
                    c.PhoneNumber == entity.PhoneNumber
                );

                Utility.AlreadyExist(customer, "Customer");
                entity = this.Normalize(entity);
            }
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the Customer. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(CustomerDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the Customer. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(CustomerUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var Customer = await _repository.RetrieveAsync(g => g.CustomerId == entity.CustomerId);

            Utility.DoesExist(Customer, "Customer");

            entity = this.Normalize(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the Customer. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    private T Normalize<T>(T entity)
    {
        if (entity is CustomerCreateDTO create)
        {
            create.FirstName = create.FirstName?.Trim().ToLower();
            create.LastName = create.LastName?.Trim().ToLower();
            create.PhoneNumber = create.PhoneNumber?.Trim().ToLower();
        }

        if (entity is CustomerUpdateDTO update)
        {
            update.FirstName = update.FirstName?.Trim().ToLower();
            update.LastName = update.LastName?.Trim().ToLower();
            update.PhoneNumber = update.PhoneNumber?.Trim().ToLower();
        }

        return entity;
    }
}
