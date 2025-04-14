using System.Data;
using AutoMapper;
using Entities.Models;
using Entities.Models.DTOs;
using FluentValidation;
using IRepository.Generic;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class GovernorateCreateValidation : AbstractValidator<GovernorateCreateDTO>
{
    public GovernorateCreateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}

public class GovernorateUpdateValidation : AbstractValidator<GovernorateUpdateDTO>
{
    public GovernorateUpdateValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");
        RuleFor(x => x.GovernorateId).NotEmpty().WithMessage("Governorate ID is required.");
        RuleFor(x => x.GovernorateId)
            .LessThan(1)
            .WithMessage("Governorate ID must be greater than 0.");

        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
    }
}

public class GovernorateDeleteValidation : AbstractValidator<GovernorateDeleteDTO>
{
    public GovernorateDeleteValidation()
    {
        RuleFor(x => x).Null().WithMessage("entity is null");

        RuleFor(x => x.GovernorateId).NotEmpty().WithMessage("Governorate ID is required.");
        RuleFor(x => x.GovernorateId)
            .LessThan(1)
            .WithMessage("Governorate ID must be greater than 0.");
    }
}

public class CreateGovernorateValidation : IGovernorateValidation
{
    private readonly IBaseRepository<Governorate> _repository;

    private readonly ILogger<CreateGovernorateValidation> _logger;
    private readonly IValidator<GovernorateCreateDTO> _createValidator;
    private readonly IValidator<GovernorateUpdateDTO> _updateValidator;
    private readonly IValidator<GovernorateDeleteDTO> _deleteValidator;

    public CreateGovernorateValidation(
        IBaseRepository<Governorate> repository,
        ILogger<CreateGovernorateValidation> logger,
        IValidator<GovernorateCreateDTO> createValidator,
        IValidator<GovernorateUpdateDTO> updateValidator,
        IValidator<GovernorateDeleteDTO> deleteValidator
    )
    {
        _repository = repository;
        _logger = logger;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _deleteValidator = deleteValidator;
    }

    public async Task ValidateCreate(GovernorateCreateDTO entity)
    {
        try
        {
            await _createValidator.ValidateAndThrowAsync(entity);

            entity = this.Normalize(entity);

            var governorate = await _repository.RetrieveAsync(g => g.Name == entity.Name);

            Utility.AlreadyExist(governorate, "Governorate");
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the creation of the governorate. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateDelete(GovernorateDeleteDTO entity)
    {
        try
        {
            await _deleteValidator.ValidateAndThrowAsync(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the deletion of the governorate. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    public async Task ValidateUpdate(GovernorateUpdateDTO entity)
    {
        try
        {
            await _updateValidator.ValidateAndThrowAsync(entity);

            var governorate = await _repository.RetrieveAsync(g =>
                g.GovernorateId == entity.GovernorateId
            );

            Utility.DoesExist(governorate, "Governorate");

            entity = this.Normalize(entity);
        }
        catch (Exception ex)
        {
            string msg =
                $"An error occurred while validating the update of the governorate. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

    private T Normalize<T>(T entity)
    {
        if (entity is ColorCreateDTO create)
        {
            create.Name = create.Name?.Trim().ToLower();
        }

        if (entity is ColorUpdateDTO update)
        {
            update.Name = update.Name?.Trim().ToLower();
        }

        return entity;
    }
}
