using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

public class DeleteOrderItemValidation : IDeleteOrderItemValidation
{
    private readonly IRetrieveOrderItem _getOrderItem;
    private readonly ILogger<DeleteOrderItemValidation> _logger;

    public DeleteOrderItemValidation(IRetrieveOrderItem getOrderItem, ILogger<DeleteOrderItemValidation> logger)
    {
        _getOrderItem = getOrderItem;
        _logger = logger;
    }

    public async Task Validate(OrderItemDeleteDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.OrderItemId);

            var orderItem = await _getOrderItem.GetByAsync(oi => oi.OrderItemId == entity.OrderItemId);

            Utility.DoesExist(orderItem, "Order Item");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the deletion of the order item. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
