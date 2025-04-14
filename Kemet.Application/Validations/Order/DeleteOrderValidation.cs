using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// Order Delete Validation
public class DeleteOrderValidation : IDeleteOrderValidation
{
    private readonly IRetrieveOrder _getOrder;
    private readonly ILogger<DeleteOrderValidation> _logger;

    public DeleteOrderValidation(IRetrieveOrder getOrder, ILogger<DeleteOrderValidation> logger)
    {
        _getOrder = getOrder;
        _logger = logger;
    }

    public async Task Validate(OrderDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.OrderId);

            var order = await _getOrder.GetByAsync(o => o.OrderId == entity.OrderId);

            Utility.DoesExist(order, "Order");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the deletion of the order. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
