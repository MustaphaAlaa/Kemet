using System.Security.Cryptography.X509Certificates;
using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using FluentValidation;
using IRepository.Generic;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class SizeValidation : ISizeValidation
{
    private readonly IBaseRepository<Size> _repository;
    private readonly ILogger<SizeValidation> _logger;
    private readonly IValidator<SizeCreateDTO> _createSizeValidator;
    private readonly IValidator<SizeUpdateDTO> _updateSizeValidator;
    private readonly IValidator<SizeDeleteDTO> _deleteSizeValidator;

    public SizeValidation(
        IBaseRepository<Size> repository,
        ILogger<SizeValidation> logger,
        IValidator<SizeCreateDTO> createSizeValidator,
        IValidator<SizeUpdateDTO> updateSizeValidator,
        IValidator<SizeDeleteDTO> deleteSizeValidator
    )
    {
        _repository = repository;
        _logger = logger;
        _createSizeValidator = createSizeValidator;
        _updateSizeValidator = updateSizeValidator;
        _deleteSizeValidator = deleteSizeValidator;
    }

    public async Task ValidateCreate(SizeCreateDTO entity)
    {
        var validator = await _createSizeValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        if (!string.IsNullOrEmpty(entity.Name))
            entity.Name = entity.Name.Trim().ToLower();

        var size = await _repository.RetrieveAsync(c => c.Name == entity.Name);

        Utility.AlreadyExist(size, "Size");
    }

    public async Task ValidateDelete(SizeDeleteDTO entity)
    {
        var validator = await _deleteSizeValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);
    }

    public async Task ValidateUpdate(SizeUpdateDTO entity)
    {
        var validator = await _updateSizeValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);

        entity.Name = entity.Name?.Trim().ToLower();

        var Size = await _repository.RetrieveAsync(c => c.SizeId == entity.SizeId);

        Utility.DoesExist(Size);
    }
}
