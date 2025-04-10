using Entities.Models.DTOs;
using IServices.IGovernorateServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// Governorate Delete Validation
public class DeleteGovernorateValidation : IDeleteGovernorateValidation
{
    private readonly IRetrieveGovernorate _getGovernorate;
    private readonly ILogger<DeleteGovernorateValidation> _logger;

    public DeleteGovernorateValidation(IRetrieveGovernorate getGovernorate, ILogger<DeleteGovernorateValidation> logger)
    {
        _getGovernorate = getGovernorate;
        _logger = logger;
    }

    public async Task Validate(GovernorateDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.GovernorateId);
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the deletion of the governorate. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
