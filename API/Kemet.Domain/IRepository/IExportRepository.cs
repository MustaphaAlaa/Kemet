using System.Linq.Expressions;
using Entities;
using Entities.Models;
using IRepository.Generic;

namespace IRepository;

public interface IExportRepository : IBaseRepository<Order>
{
    Task<List<OrderDetailsForExport>> GetOrderDetailsForExport(List<int> orderIds);
    Task<List<int>> ValidateOrdersHaveRequiredFields(List<int> orderIds);
}
