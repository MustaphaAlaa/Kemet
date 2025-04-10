﻿using Entities.Models.DTOs;
using IServices.IColorServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;


public class CreateColorValidation : ICreateColorValidation
{
    private readonly IRetrieveColor _getColor;
    private readonly ILogger<CreateColorValidation> _logger;

    public CreateColorValidation(IRetrieveColor getColor, ILogger<CreateColorValidation> logger)
    {
        _getColor = getColor;
        _logger = logger;
    }

    public async Task Validate(ColorCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IsNullOrEmpty(entity.NameEn, "Color's English name");
            Utility.IsNullOrEmpty(entity.NameAr, "Color's Arabic name");
            Utility.IsNullOrEmpty(entity.Hexacode, "Hexacode");


            entity.NameAr = entity.NameAr?.Trim().ToLower();
            entity.NameEn = entity.NameEn?.Trim().ToLower();
            entity.Hexacode = entity.Hexacode?.Trim().ToLower();


            var color = await _getColor.GetByAsync(c => c.Hexacode == entity.Hexacode || (c.NameAr == entity.NameAr) || c.NameEn == entity.NameAr);

            Utility.AlreadyExist(color, "Color");
        }
        catch (Exception ex)
        {
            string msg = $"An error throwed while validating the creation of the color. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
