using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using IRepository.Generic;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;
using Application.Exceptions;
using System.Net.Http.Headers;

namespace Entities.Models.Validations;

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

        var validator = await _createValidator.ValidateAsync(entity);

        if (!validator.IsValid)
        {
            throw new ValidationException(validator.Errors);
        }


        entity = this.Normalize(entity);

        var color = await _repository.RetrieveAsync(c =>
            c.HexaCode == entity.HexaCode || (c.Name == entity.Name)
        );

        Utility.AlreadyExist(color, "Color"); //AlreadyExistException

    }

    public async Task ValidateDelete(ColorDeleteDTO entity)
    {

        var validator = await _deleteValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);



    }

    public async Task ValidateUpdate(ColorUpdateDTO entity)
    {
        var validator = await _updateValidator.ValidateAsync(entity);

        if (!validator.IsValid)
            throw new ValidationException(validator.Errors);


        var Color = await _repository.RetrieveAsync(c => c.ColorId == entity.ColorId);

        Utility.DoesExist(Color, "Color");

        entity = this.Normalize(entity);

    }

    private T Normalize<T>(T entity)
    {
        if (entity is ColorCreateDTO create)
        {
            create.Name = create.Name?.Trim().ToLower();
            create.HexaCode = create.HexaCode?.Trim().ToLower();
        }

        if (entity is ColorUpdateDTO update)
        {
            update.Name = update.Name?.Trim().ToLower();
            update.HexaCode = update.HexaCode?.Trim().ToLower();
        }

        return entity;
    }
}
