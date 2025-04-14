using Entities.Models.DTOs;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// Order Update Validation
public class UpdateOrderValidation : IUpdateOrderValidation
{
    private readonly IRetrieveOrder _getOrder;
    private readonly ILogger<UpdateOrderValidation> _logger;

    public UpdateOrderValidation(IRetrieveOrder getOrder, ILogger<UpdateOrderValidation> logger)
    {
        _getOrder = getOrder;
        _logger = logger;
    }

    public async Task<OrderReadDTO> Validate(OrderUpdateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.OrderId);

            var order = await _getOrder.GetByAsync(o => o.OrderId == entity.OrderId);

            Utility.DoesExist(order, "Order");

            return order;
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the update of the order. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
