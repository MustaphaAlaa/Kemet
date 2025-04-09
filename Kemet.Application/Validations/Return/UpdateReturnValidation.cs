using Entities.Models.DTOs;
using IServices.IReturnServices;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

// Return Update Validation
public class UpdateReturnValidation : IUpdateReturnValidation
{
    private readonly IRetrieveReturn _getReturn;
    private readonly ILogger<UpdateReturnValidation> _logger;

    public UpdateReturnValidation(IRetrieveReturn getReturn, ILogger<UpdateReturnValidation> logger)
    {
        _getReturn = getReturn;
        _logger = logger;
    }

    public async Task<ReturnReadDTO> Validate(ReturnUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ReturnId);

            var returnEntity = await _getReturn.GetByAsync(r => r.ReturnId == entity.ReturnId);

            Utility.DoesExist(returnEntity, "Return");

            return returnEntity;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the update of the return. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
