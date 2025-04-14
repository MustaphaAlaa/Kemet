
using Entities.Models;
using Entities.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Entities.Infrastructure.Extensions;
using Entities.Models.Extensions;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();




builder.Services.AddInfraStructure(builder.Configuration);
builder.Services.AddApplicationLayer();


// Configure Identity 
builder.Services.AddIdentity<User, Role>(options =>
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


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
