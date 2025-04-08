using AutoMapper;
using Entities.Models.DTOs;
using IServices.IGovernorateServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// Governorate Create Validation
public class CreateGovernorateValidation : ICreateGovernorateValidation
{
    private readonly IRetrieveGovernorate _getGovernorate;
    private readonly ILogger<CreateGovernorateValidation> _logger;

    public CreateGovernorateValidation(IRetrieveGovernorate getGovernorate, ILogger<CreateGovernorateValidation> logger)
    {
        _getGovernorate = getGovernorate;
        _logger = logger;
    }

    public async Task Validate(GovernorateCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IsNullOrEmpty(entity.NameAr, "Governorate Arabic Name");
            Utility.IsNullOrEmpty(entity.NameEn, "Governorate English Name");

            entity.NameAr = entity.NameAr?.Trim().ToLower();
            entity.NameEn = entity.NameEn?.Trim().ToLower();

            var governorate = await _getGovernorate.GetByAsync(g => g.NameAr == entity.NameAr || g.NameEn == entity.NameEn);

            Utility.AlreadyExist(governorate, "Governorate");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the creation of the governorate. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
