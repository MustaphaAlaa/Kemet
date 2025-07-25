using Entities.Infrastructure;
using Entities.Infrastructure.Extensions;
using Entities.Models;
using Entities.Models.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfraStructure(builder.Configuration);
builder.Services.AddApplicationLayer();

// Configure Identity
builder
    .Services.AddIdentity<User, Role>(options =>
    {
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
    })
    .AddEntityFrameworkStores<KemetDbContext>()
    .AddUserStore<UserStore<User, Role, KemetDbContext, int>>()
    .AddRoleStore<RoleStore<Role, KemetDbContext, int>>();

//.AddDefaultTokenProviders();

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.



app.UseCors(options =>
{
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        //.WithOrigins(builder.Configuration.GetSection("Clients").Get();
        .WithOrigins("https://localhost:3000");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
