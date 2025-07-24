using IRepository.Generic;
using Entities.Infrastructure;
using IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Generic;

namespace Entities.Infrastructure.Extensions;

public static class Infrastructure
{
    public static void AddInfraStructure(
        this IServiceCollection service,
        IConfiguration configuration
    )
    {
        AddDb(service, configuration);
        AddRepositories(service);
    }

    private static void AddDb(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("KemetDb");
        service.AddDbContext<KemetDbContext>(options => options.UseNpgsql(connectionString));
    }

    private static void AddRepositories(this IServiceCollection service)
    {
        service.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        service.AddTransient(typeof(IRangeRepository<>), typeof(RangeRepository<>));
        service.AddTransient<IDeliveryCompanyRepository, DeliveryCompanyRepository>();
        service.AddTransient<IGovernorateDeliveryRepository, GovernorateDeliveryRepository>();
        service.AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
