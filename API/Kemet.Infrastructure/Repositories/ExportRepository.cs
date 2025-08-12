using System.Text;
using System.Threading.Tasks;
using Entities;
using Entities.Infrastructure;
using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repositories.Generic;

public class ExportRepository : BaseRepository<Order>, IExportRepository
{
    private readonly KemetDbContext _db;
    private ILogger<ExportRepository> _logger;

    public ExportRepository(KemetDbContext context, ILogger<ExportRepository> logger)
        : base(context)
    {
        _db = context;
        _logger = logger;
    }

    private async Task<List<OrderDetailsForExport>> GetOrder(List<int> orderIds)
    {
        var orderDetailsQuery =
            from order in _db.Orders
            join customer in _db.Customers on order.CustomerId equals customer.CustomerId
            join address in _db.Addresses on order.AddressId equals address.AddressId
            join governorateDelivery in _db.GovernorateDelivery
                on order.GovernorateDeliveryId equals governorateDelivery.GovernorateDeliveryId
            join governorate in _db.Governorates
                on governorateDelivery.GovernorateId equals governorate.GovernorateId
            join governorateDeliveryCompany in _db.GovernorateDeliveryCompanies
                on order.GovernorateDeliveryCompanyId equals governorateDeliveryCompany.GovernorateDeliveryCompanyId
            join deliveryCompany in _db.DeliveryCompanies
                on order.DeliveryCompanyId equals deliveryCompany.DeliveryCompanyId
            join productQuantityPrice in _db.ProductQuantityPrices
                on order.ProductQuantityPriceId equals productQuantityPrice.ProductQuantityPriceId
            where orderIds.Contains(order.OrderId)
            select new OrderDetailsForExport
            {
                OrderId = order.OrderId,
                CodeFromDeliveryCompany = order.CodeFromDeliveryCompany,
                OrderNote = order.Note,
                OrderTotalPrice = order.OrderTotalPrice,
                DeliveryCostForCompany = governorateDeliveryCompany.DeliveryCost ?? 0,
                DeliveryCostForCustomer = governorateDelivery.DeliveryCost ?? 0,
                GovernorateName = governorate.Name,
                StreetAddress = order.Address.StreetAddress,
                CustomerName = order.Customer.FirstName + " " + order.Customer.LastName,
                CustomerPhoneNumber = order.Customer.PhoneNumber,
                orderItemsQuantity = order.ProductQuantityPrice.Quantity,
            };

        var ordersDetails = await orderDetailsQuery.ToListAsync();

        return ordersDetails;
    }

    private async Task<string> GetOrderItems(int orderId)
    {
        StringBuilder orderItemString = new();
        var orderItemsQuery =
            from orderItem in _db.OrderItems
            where orderItem.OrderId == orderId
            join productVariant in _db.ProductVariants
                on orderItem.ProductVariantId equals productVariant.ProductVariantId
            join product in _db.Products on productVariant.ProductId equals product.ProductId
            join color in _db.Colors on productVariant.ColorId equals color.ColorId
            join size in _db.Sizes on productVariant.SizeId equals size.SizeId
            select new
            {
                ColorName = orderItem.ProductVariant.Color.Name,
                SizeName = orderItem.ProductVariant.Size.Name,
                ProductName = orderItem.ProductVariant.Product.Name,
                Quantity = orderItem.Quantity,
            };

        var orderDetails = await orderItemsQuery.ToListAsync();

        if (!orderDetails.Any())
        {
            throw new Exception("There's no order Items to get.");
        }

        foreach (var item in orderDetails)
        {
            orderItemString.AppendLine($"{item.ProductName} - Color: {item.ColorName} - Size: {item.SizeName} - Quantity: {item.Quantity}  .");
        }

        var str = orderItemString.ToString();
        return str;
    }

    public async Task<List<int>> ValidateOrdersHaveRequiredFields(List<int> orderIds)
    {
        var invalidOrders = await _db
            .Orders.Where(o => orderIds.Contains(o.OrderId))
            .Where(o =>
                o.DeliveryCompanyId == null
                || o.GovernorateDeliveryCompanyId == null
                || o.GovernorateDeliveryId == null
                || o.AddressId == null
                || o.CustomerId == null
                || string.IsNullOrWhiteSpace(o.CodeFromDeliveryCompany)
            )
            .Select(o => o.OrderId)
            .ToListAsync();

        return invalidOrders;
    }

    public async Task<List<OrderDetailsForExport>> GetOrderDetailsForExport(List<int> orderIds)
    {
        try
        {
            List<OrderDetailsForExport> OrdersDetailsForExports = new();
            var orders = await this.GetOrder(orderIds);
            foreach (var order in orders)
            {
                if (order is null)
                    continue;
                var orderItemsString = await GetOrderItems(order.OrderId);
                order.OrderDetails = orderItemsString;
                OrdersDetailsForExports.Add(order);
            }
            return OrdersDetailsForExports;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message, ex);
            throw;
        }
    }
}
