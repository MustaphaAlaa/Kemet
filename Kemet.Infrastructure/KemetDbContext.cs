using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Kemet.Infrastructure;

public class KemetDbContext : IdentityDbContext<User,Role, int> 
{
    private readonly IConfiguration _configuration;

    public KemetDbContext(DbContextOptions<KemetDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("KemetDb"));
    }
}

