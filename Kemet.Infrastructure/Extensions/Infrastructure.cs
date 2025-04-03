using Entities.Models;
using Kemet.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kemet.Intrastructure.Extensions;

public static class Infrastructure
{
    public static void AddInfraStructure(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("KemetDb");

        service.AddDbContext<KemetDbContext>(options => options.UseNpgsql(connectionString));


    }
}