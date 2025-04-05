using Entities.Models;
using Kemet.Application.Extensions;
using Kemet.Infrastructure;
using Kemet.Intrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

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
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
