using Entities.Models;
using IRepository.Generic;

namespace IRepository;

public interface IDeliveryCompanyRepository : IBaseRepository<DeliveryCompany>
{
    IQueryable<GovernorateDeliveryCompany> ActiveGovernorates(int deliveryCompanyId);
}
