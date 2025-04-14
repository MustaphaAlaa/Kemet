using Entities.Models.DTOs;

using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// OrderItem Validation
public class CreateOrderItemValidation : ICreateOrderItemValidation
{
    private readonly IRetrieveOrderItem _getOrderItem;
    private readonly ILogger<CreateOrderItemValidation> _logger;

    public CreateOrderItemValidation(IRetrieveOrderItem getOrderItem, ILogger<CreateOrderItemValidation> logger)
    {
        _getOrderItem = getOrderItem;
        _logger = logger;
    }

    public async Task Validate(OrderItemCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.ProductVariantId);
            Utility.IsPositive(entity.Quantity, "Order Item Quantity");
            Utility.IsPositive(entity.UnitPrice, "Order Item Unit Price");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the creation of the order item. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
