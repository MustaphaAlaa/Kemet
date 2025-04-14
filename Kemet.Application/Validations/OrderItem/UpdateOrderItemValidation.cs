using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// OrderItem Update Validation
public class UpdateOrderItemValidation : IUpdateOrderItemValidation
{
    private readonly IRetrieveOrderItem _getOrderItem;
    private readonly ILogger<UpdateOrderItemValidation> _logger;

    public UpdateOrderItemValidation(IRetrieveOrderItem getOrderItem, ILogger<UpdateOrderItemValidation> logger)
    {
        _getOrderItem = getOrderItem;
        _logger = logger;
    }

    public async Task<OrderItemReadDTO> Validate(OrderItemUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.OrderItemId);

            var orderItem = await _getOrderItem.GetByAsync(oi => oi.OrderItemId == entity.OrderItemId);

            Utility.DoesExist(orderItem, "Order Item");

            return orderItem;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the update of the order item. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
