using Entities.Models.DTOs;
using IServices.IReturnServices;

using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// Return Validation
public class CreateReturnValidation : ICreateReturnValidation
{
    private readonly IRetrieveReturn _getReturn;
    private readonly ILogger<CreateReturnValidation> _logger;

    public CreateReturnValidation(IRetrieveReturn getReturn, ILogger<CreateReturnValidation> logger)
    {
        _getReturn = getReturn;
        _logger = logger;
    }

    public async Task Validate(ReturnCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.OrderItemId);
            Utility.IsPositive(entity.Quantity, "Return Quantity");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the creation of the return. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
