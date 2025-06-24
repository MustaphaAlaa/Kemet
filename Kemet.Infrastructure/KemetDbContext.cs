using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Entities.Infrastructure;

public class KemetDbContext : IdentityDbContext<User, Role, int>
{
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Governorate> Governorates { get; set; }

    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Return> Returns { get; set; }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }

    public DbSet<Color> Colors { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Customer> Customers { get; set; }

    public DbSet<DeliveryCompany> DeliveryCompanies { get; set; }
    public DbSet<GovernorateDeliveryCompany> GovernorateDeliveryCompanies { get; set; }
    public DbSet<GovernorateDelivery> GovernorateDelivery { get; set; }

    public DbSet<OrderReceiptNote> OrderReceiptNotes { get; set; }
    public DbSet<OrderReceiptStatus> OrderReceiptStatuses { get; set; }

    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public DbSet<Price> Prices { get; set; }
    public DbSet<ProductQuantityPrice> ProductQuantityPrices { get; set; }

    private readonly IConfiguration _configuration;

    public KemetDbContext(DbContextOptions<KemetDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseNpgsql(_configuration.GetConnectionString("KemetDb"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(KemetDbContext).Assembly);

        builder
            .Entity<Order>()
            .HasMany(Items => Items.OrderItems)
            .WithOne(Item => Item.Order)
            .HasForeignKey(Item => Item.OrderId);
    }
}
