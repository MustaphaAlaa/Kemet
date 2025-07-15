using System.Data.Common;
using Entities.Infrastructure;
using Entities.Models;
using IRepository;

namespace Repositories.Generic;

public class GovernorateDeliveryRepository
    : BaseRepository<GovernorateDelivery>,
        IGovernorateDeliveryRepository
{
    // protected readonly KemetDbContext _db;
    private readonly KemetDbContext _db;

    public GovernorateDeliveryRepository(KemetDbContext context)
        : base(context)
    {
        _db = context;
    }

    public IQueryable<GovernorateDelivery> ActiveGovernoratesDelivery()
    {
        return _db
            .GovernorateDelivery.Where(governorateDelivery => governorateDelivery.IsActive == true)
            .Join(
                _db.Governorates,
                governorateDelivery => governorateDelivery.GovernorateId,
                governorate => governorate.GovernorateId,
                (governorateDelivery, governorate) =>
                    new GovernorateDelivery
                    {
                        GovernorateDeliveryId = governorateDelivery.GovernorateDeliveryId,
                        DeliveryCost = governorateDelivery.DeliveryCost,
                        IsActive = governorateDelivery.IsActive,
                        CreatedAt = governorateDelivery.CreatedAt,
                        GovernorateId = governorate.GovernorateId,
                        Governorate = governorate,
                    }
            );
    }

    public IQueryable<GovernorateDelivery> NullableActiveGovernoratesDelivery()
    {
        return _db
            .GovernorateDelivery.Where(governorateDelivery =>
                governorateDelivery.IsActive == true || governorateDelivery.IsActive == null
            )
            .Join(
                _db.Governorates,
                governorateDelivery => governorateDelivery.GovernorateId,
                governorate => governorate.GovernorateId,
                (governorateDelivery, governorate) =>
                    new GovernorateDelivery
                    {
                        GovernorateDeliveryId = governorateDelivery.GovernorateDeliveryId,
                        DeliveryCost = governorateDelivery.DeliveryCost,
                        IsActive = governorateDelivery.IsActive,
                        CreatedAt = governorateDelivery.CreatedAt,
                        GovernorateId = governorate.GovernorateId,
                        Governorate = governorate,
                    }
            );
    }
}
