using Entities.Models.DTOs;
using IServices.IGovernorateServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

public class UpdateGovernorateValidation : IUpdateGovernorateValidation
{
    private readonly IRetrieveGovernorate _getGovernorate;
    private readonly ILogger<UpdateGovernorateValidation> _logger;

    public UpdateGovernorateValidation(IRetrieveGovernorate getGovernorate, ILogger<UpdateGovernorateValidation> logger)
    {
        _getGovernorate = getGovernorate;
        _logger = logger;
    }

    public async Task<GovernorateReadDTO> Validate(GovernorateUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);

            Utility.IdBoundry(entity.GovernorateId);

            Utility.IsNullOrEmpty(entity.NameAr, "Governorate Arabic Name");
            Utility.IsNullOrEmpty(entity.NameEn, "Governorate English Name");

            entity.NameAr = entity.NameAr?.Trim().ToLower();
            entity.NameEn = entity.NameEn?.Trim().ToLower();

            var governorate = await _getGovernorate.GetByAsync(g => g.GovernorateId == entity.GovernorateId);

            Utility.DoesExist(governorate, "Governorate");

            return governorate;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the update of the governorate. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
