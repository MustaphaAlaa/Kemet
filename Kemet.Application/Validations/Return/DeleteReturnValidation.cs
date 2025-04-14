using Entities.Models.DTOs;
using IServices.IReturnServices;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// Return Delete Validation
public class DeleteReturnValidation : IDeleteReturnValidation
{
    private readonly IRetrieveReturn _getReturn;
    private readonly ILogger<DeleteReturnValidation> _logger;

    public DeleteReturnValidation(IRetrieveReturn getReturn, ILogger<DeleteReturnValidation> logger)
    {
        _getReturn = getReturn;
        _logger = logger;
    }

    public async Task Validate(ReturnDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ReturnId);

            var returnEntity = await _getReturn.GetByAsync(r => r.ReturnId == entity.ReturnId);

            Utility.DoesExist(returnEntity, "Return");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the deletion of the return. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
