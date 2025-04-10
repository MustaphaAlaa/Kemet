﻿using Entities.Models.DTOs;
using IServices.ISizeServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;



namespace Kemet.Application.Validations;


public class CreateSizeValidation : ICreateSizeValidation
{
    private readonly IRetrieveSize _getSize;
    private readonly ILogger<CreateSizeValidation> _logger;

    public CreateSizeValidation(IRetrieveSize getSize, ILogger<CreateSizeValidation> logger)
    {
        _getSize = getSize;
        _logger = logger;
    }

    public async Task Validate(SizeCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IsNullOrEmpty(entity.Name, "Size");

            entity.Name = entity.Name?.Trim().ToLower();

            var size = await _getSize.GetByAsync(c => c.Name == entity.Name);

            Utility.AlreadyExist(size, "Size");
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while validating the creation of the size. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }

}
