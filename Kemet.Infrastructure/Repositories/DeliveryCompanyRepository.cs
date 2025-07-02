using Entities.Infrastructure;
using Entities.Models;
using IRepository;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Generic;

public class DeliveryCompanyRepository : BaseRepository<DeliveryCompany>, IDeliveryCompanyRepository
{
    // protected readonly KemetDbContext _db;
    private readonly KemetDbContext _db;

    public DeliveryCompanyRepository(KemetDbContext context)
        : base(context)
    {
        _db = context;
    }

    public IQueryable<GovernorateDeliveryCompany> ActiveGovernorates(int deliveryCompanyId)
    {
        return _db
            .DeliveryCompanies.Where(deliveryCompany =>
                deliveryCompany.DeliveryCompanyId == deliveryCompanyId
            )
            .Join(
                _db.GovernorateDeliveryCompanies,
                deliveryCompany => deliveryCompany.DeliveryCompanyId,
                governorateDeliveryCompany => governorateDeliveryCompany.DeliveryCompanyId,
                (deliveryCompany, governorateDeliveryCompany) =>
                    new { deliveryCompany, governorateDeliveryCompany }
            )
            .Where(d =>
                d.governorateDeliveryCompany.IsActive == true
                || d.governorateDeliveryCompany.IsActive == null
            )
            .Join(
                _db.Governorates,
                temp => temp.governorateDeliveryCompany.GovernorateId,
                governorate => governorate.GovernorateId,
                (combined, temp) =>
                    new GovernorateDeliveryCompany
                    {
                        DeliveryCompanyId = combined.deliveryCompany.DeliveryCompanyId,
                        DeliveryCompany = combined.deliveryCompany,
                        CreatedAt = combined.deliveryCompany.DialingWithItFrom,
                        IsActive = combined.governorateDeliveryCompany.IsActive,
                        DeliveryCost = combined.governorateDeliveryCompany.DeliveryCost,
                        GovernorateDeliveryCompanyId = combined
                            .governorateDeliveryCompany
                            .GovernorateDeliveryCompanyId,
                        GovernorateId = temp.GovernorateId,
                        Governorate = temp,
                    }
            );
    }
}
