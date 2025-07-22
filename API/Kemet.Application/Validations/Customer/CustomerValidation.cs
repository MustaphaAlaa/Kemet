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
        var validator = await _createValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        // if (
        //     (entity.UserId == Guid.Empty || entity.UserId == null)
        //     && String.IsNullOrEmpty(entity.FirstName)
        //     && String.IsNullOrEmpty(entity.LastName)
        //     && String.IsNullOrEmpty(entity.PhoneNumber)
        // )
        // {
        //     throw new ValidationException("All Customer data is empty or null or invalid");
        // }

        // if (
        //     entity.UserId == Guid.Empty
        //     && (
        //         String.IsNullOrEmpty(entity.FirstName)
        //         || String.IsNullOrEmpty(entity.LastName)
        //         || String.IsNullOrEmpty(entity.PhoneNumber)
        //     )
        // )
        // {
        //     throw new ValidationException("Anonymous user data is empty or null");
        // }

        if (entity.UserId != Guid.Empty && entity.UserId is not null)
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

    public async Task ValidateDelete(CustomerDeleteDTO entity)
    {
        var validator = await _deleteValidator.ValidateAsync(entity);

        var valid = entity.CustomerId != Guid.Empty || entity.PhoneNumber is not null;

        if (!validator.IsValid || !valid)
            throw new ValidationException(validator.Errors);
    }

    public async Task ValidateUpdate(CustomerUpdateDTO entity)
    {
        var validator = await _updateValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        var Customer = await _repository.RetrieveAsync(g => g.CustomerId == entity.CustomerId);

        Utility.DoesExist(Customer, "Customer");

        entity = this.Normalize(entity);
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
