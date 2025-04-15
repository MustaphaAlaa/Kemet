using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class AddressValidation : IAddressValidation
{
    private readonly IBaseRepository<Address> _repository;
    private readonly IBaseRepository<Governorate> _governorateRepository;
    private readonly IBaseRepository<Customer> _customerRepository;

    private readonly ILogger<AddressValidation> _logger;
    private readonly IValidator<AddressCreateDTO> _createValidator;
    private readonly IValidator<AddressUpdateDTO> _updateValidator;
    private readonly IValidator<AddressDeleteDTO> _deleteValidator;

    public AddressValidation(
        IBaseRepository<Address> repository,
        IBaseRepository<Governorate> governorateRepository,
        IBaseRepository<Customer> customerRepository,
        ILogger<AddressValidation> logger,
        IValidator<AddressCreateDTO> createValidator,
        IValidator<AddressUpdateDTO> updateValidator,
        IValidator<AddressDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _governorateRepository = governorateRepository;
        _customerRepository = customerRepository;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(AddressCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            var governorate = await _governorateRepository.RetrieveAsync(g =>
                g.GovernorateId == entity.GovernorateId
            );

            Utility.DoesExist(governorate);

            var customer = await _customerRepository.RetrieveAsync(c =>
                c.CustomerId == entity.CustomerId
            );

            Utility.DoesExist(customer);

            entity.StreetAddress = entity.StreetAddress.Trim().ToLower();
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the Address. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(AddressDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the Address. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(AddressUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var Address = await _repository.RetrieveAsync(address =>
                address.AddressId == entity.AddressId
            );

            Utility.DoesExist(Address, "Address");

            entity.StreetAddress = entity.StreetAddress.Trim().ToLower();
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the Address. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
