using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using IRepository.Generic;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

public class ColorValidation : IColorValidation
{
    private readonly ILogger<ColorValidation> _logger;
    private readonly IBaseRepository<Color> _repository;
    private readonly IValidator<ColorCreateDTO> _createValidator;
    private readonly IValidator<ColorUpdateDTO> _updateValidator;
    private readonly IValidator<ColorDeleteDTO> _deleteValidator;

    public ColorValidation(
        ILogger<ColorValidation> logger,
        IBaseRepository<Color> repository,
        IValidator<ColorCreateDTO> createValidator,
        IValidator<ColorUpdateDTO> updateValidator,
        IValidator<ColorDeleteDTO> deleteValidator
    )
    {
        _logger = logger;
        _repository = repository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(ColorCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            entity = this.Normalize(entity);

            var color = await _repository.RetrieveAsync(c =>
                c.Hexacode == entity.Hexacode
                || (c.NameAr == entity.NameAr)
                || c.NameEn == entity.NameAr
            );

            Utility.AlreadyExist(color, "Color");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error throwed while validating the creation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(ColorDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error throwed while validating the creation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(ColorUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var Color = await _repository.RetrieveAsync(c => c.ColorId == entity.ColorId);

            Utility.DoesExist(Color, "Color");

            entity = this.Normalize(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error throwed while validating the updation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    private T Normalize<T>(T entity)
    {
        if (entity is ColorCreateDTO create)
        {
            create.NameAr = create.NameAr?.Trim().ToLower();
            create.NameEn = create.NameEn?.Trim().ToLower();
            create.Hexacode = create.Hexacode?.Trim().ToLower();
        }

        if (entity is ColorUpdateDTO update)
        {
            update.NameAr = update.NameAr?.Trim().ToLower();
            update.NameEn = update.NameEn?.Trim().ToLower();
            update.Hexacode = update.Hexacode?.Trim().ToLower();
        }

        return entity;
    }
}
