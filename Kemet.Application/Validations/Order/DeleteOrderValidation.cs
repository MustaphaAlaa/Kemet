using Entities.Models.DTOs;
using Kemet.Application.Interfaces.Validations;
using Kemet.Application.Utilities;
using Microsoft.Extensions.Logging;

namespace Kemet.Application.Validations;

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
