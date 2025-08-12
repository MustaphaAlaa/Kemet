using ClosedXML.Excel;
using Entities;
using Entities.Models.Interfaces.Helpers;
using IRepository;
using Microsoft.Extensions.Logging;

namespace Application.Services;

 

public class ExportToExcelService : IExport
{
    private readonly IExportRepository _exportRepository;
    protected readonly ILogger<ExportToExcelService> _logger;

    public ExportToExcelService(
        IExportRepository exportRepository,
        ILogger<ExportToExcelService> logger
    )
    {
        _exportRepository = exportRepository;
        _logger = logger;
    }

    private async Task ValidateOrdersHaveRequiredFields(List<int> orderIds)
    {
        try
        {
            var invalidOrders = await _exportRepository.ValidateOrdersHaveRequiredFields(orderIds);

            if (invalidOrders.Any())
            {
                throw new InvalidOperationException(
                    $"The following orders are missing required data: {string.Join(", ", invalidOrders)}"
                );
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<byte[]> Export(List<int> orderIds)
    {
        try
        {
            await this.ValidateOrdersHaveRequiredFields(orderIds);
            var orderDetails = await _exportRepository.GetOrderDetailsForExport(orderIds);

            using var workbook = new XLWorkbook();

            var worksheet = workbook.AddWorksheet("orders");

            string[] headers =
            [
                "رقم الطلب",   // 1
                "كود شركة الشحن", //2
                "اسم العميل", //3 
                "رقم الموبايل",//4
                "العنوان", // 5
                "المحافظة", // 6 
                "ملاحظات",// 7
                "عدد القطع", // 8
                "أجمالى سعر الطلب", //9 
                "تكلفة الشحن على العميل", // 10
                "تكلفة شحن الشركة",  // 11
                "تفاصيل الطلب",  // 12
            ];

            for (int i = 0; i < headers.Length; i++)
                worksheet.Cell(1, i + 1).Value = headers[i];

            short row = 2;
            var col = 1;

            foreach (var order in orderDetails)
            {
                col = 1;
                worksheet.Cell(row, 1).Value = order.OrderId;
                worksheet.Cell(row, 2).Value = order.CodeFromDeliveryCompany;
                worksheet.Cell(row, 3).Value = order.CustomerName;
                worksheet.Cell(row, 4).Value = order.CustomerPhoneNumber;
                worksheet.Cell(row, 5).Value = order.StreetAddress;
                worksheet.Cell(row, 6).Value = order.GovernorateName;
                worksheet.Cell(row, 7).Value = order.OrderNote;
                worksheet.Cell(row, 8).Value = order.orderItemsQuantity;
                worksheet.Cell(row, 9).Value = order.OrderTotalPrice;
                worksheet.Cell(row, 10).Value = order.DeliveryCostForCustomer;
                worksheet.Cell(row, 11).Value = order.DeliveryCostForCompany;
                worksheet.Cell(row, 12).Value = order.OrderDetails;
                row++;
            }

     
            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            throw;
        }
    }
}
