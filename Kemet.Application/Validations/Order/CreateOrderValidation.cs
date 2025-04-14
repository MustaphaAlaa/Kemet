using Entities.Models.DTOs;
using IServices.IOrderServices;
using Entities.Models.Interfaces.Validations;
using Entities.Models.Utilities;
using Microsoft.Extensions.Logging;

namespace Entities.Models.Validations;

// Init Version - Notfinal
public class CreateOrderValidation : ICreateOrderValidation
{
    private readonly IRetrieveOrder _getOrder;
    private readonly ILogger<CreateOrderValidation> _logger;

    public CreateOrderValidation(IRetrieveOrder getOrder, ILogger<CreateOrderValidation> logger)
    {
        _getOrder = getOrder;
        _logger = logger;
    }

    public async Task Validate(OrderCreateDTO entity)
    {
        try
        {
            Utility.IsNull(entity);
            Utility.IdBoundry(entity.CustomerId);
            //Utility.IsNullOrEmpty(entity.OrderItems, "Order Items");
        }
        catch (Exception ex)
        {
            string msg = $"An error occurred while validating the creation of the order. {ex.Message}";
            _logger.LogError(msg);
            throw;
        }
    }
}
