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

    private readonly IValidator<AddressCreateDTO> _createValidator;
    private readonly IValidator<AddressUpdateDTO> _updateValidator;
    private readonly IValidator<AddressDeleteDTO> _deleteValidator;

    public AddressValidation(
        IBaseRepository<Address> repository,
        IBaseRepository<Governorate> governorateRepository,
        IBaseRepository<Customer> customerRepository,
        IValidator<AddressCreateDTO> createValidator,
        IValidator<AddressUpdateDTO> updateValidator,
        IValidator<AddressDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _governorateRepository = governorateRepository;
        _customerRepository = customerRepository;

        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(AddressCreateDTO entity)
    {
        var validator = await _createValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

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

    public async Task ValidateDelete(AddressDeleteDTO entity)
    {
        var validator = await _deleteValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
    }

    public async Task ValidateUpdate(AddressUpdateDTO entity)
    {
        var validator = await _updateValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        var Address = await _repository.RetrieveAsync(address =>
            address.AddressId == entity.AddressId
        );

        Utility.DoesExist(Address, "Address");

        entity.StreetAddress = entity.StreetAddress.Trim().ToLower();
    }
}
