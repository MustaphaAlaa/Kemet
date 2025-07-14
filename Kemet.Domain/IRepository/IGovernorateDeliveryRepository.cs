using Entities.Models;
using IRepository.Generic;

namespace IRepository;

public interface IGovernorateDeliveryRepository : IBaseRepository<GovernorateDelivery>
{
    IQueryable<GovernorateDelivery> ActiveGovernoratesDelivery();

    IQueryable<GovernorateDelivery> NullableActiveGovernoratesDelivery();
}
