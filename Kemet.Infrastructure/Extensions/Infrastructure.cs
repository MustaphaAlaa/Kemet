

using IRepository.Generic;
using Kemet.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repositories.Generic;
namespace Kemet.Intrastructure.Extensions;

public static class Infrastructure
{
    public static void AddInfraStructure(this IServiceCollection service, IConfiguration configuration)
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
        service.AddScoped(typeof(ICreateAsync<>), typeof(CreateRepository<>));
        service.AddScoped(typeof(IRetrieveAsync<>), typeof(RetrieveRepository<>));
        service.AddScoped(typeof(IRetrieveAllAsync<>), typeof(RetrieveAllRepository<>));
        service.AddScoped(typeof(IUpdateAsync<>), typeof(UpdateRepository<>));
        service.AddScoped(typeof(IDeleteAsync<>), typeof(DeleteRepository<>));
    }
}