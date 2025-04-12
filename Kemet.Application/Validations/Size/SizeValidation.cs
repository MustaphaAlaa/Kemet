using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using IRepository.Generic;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

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
        try
        {
            await _createSizeValidator.ValidateAndThrowAsync(entity);

            if (!string.IsNullOrEmpty(entity.Name))
            {
                entity.Name = entity.Name.Trim().ToLower();
            }

            var size = await _repository.RetrieveAsync(c => c.Name == entity.Name);

            Utility.AlreadyExist(size, "Size");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error throwed while validating the creation of the size. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(SizeDeleteDTO entity)
    {
        try
        {
            await _deleteSizeValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error throwed while validating the deleting of the size. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(SizeUpdateDTO entity)
    {
        try
        {
            await _updateSizeValidator.ValidateAndThrowAsync(entity);

            entity.Name = entity.Name?.Trim().ToLower();

            var Size = await _repository.RetrieveAsync(c => c.SizeId == entity.SizeId);

            Utility.DoesExist(Size);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error throwed while validating the updation of the size. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
