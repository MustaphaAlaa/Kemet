using System.Text;
using Entities.Infrastructure;
using Entities.Infrastructure.Extensions;
using Entities.Models;
using Entities.Models.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfraStructure(builder.Configuration);
builder.Services.AddApplicationLayer();

// Configure Identity
builder
    .Services.AddIdentity<User, Role>(options =>
    {
        options.User.RequireUniqueEmail = true;

        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;

       
    })
    .AddEntityFrameworkStores<KemetDbContext>()
    .AddUserStore<UserStore<User, Role, KemetDbContext, Guid>>()
    .AddRoleStore<RoleStore<Role, KemetDbContext, Guid>>();

// will be applied when I start using authentication
// builder.Services.AddAuthorization(options =>
//     options.FallbackPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
//         .RequireAuthenticatedUser()
//         .Build()
// );

//.AddDefaultTokenProviders();

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme =
            options.DefaultChallengeScheme =
            options.DefaultForbidScheme =
            options.DefaultScheme =
            options.DefaultSignInScheme =
            options.DefaultSignOutScheme =
                JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"])
            ),
        };
    });

builder.Services.AddCors();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options
        .AllowAnyHeader()
        .AllowAnyMethod()
        //.WithOrigins(builder.Configuration.GetSection("Clients").Get();
        .WithOrigins("https://localhost:3000");
    // .WithOrigins();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
