using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartGallery.Server.Data;
using SmartGallery.Server.Middlewares;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services;
using SmartGallery.Server.Services.Contracts;

namespace SmartGallery.Server.Extensions;
public static class ServiceExtensions
{
    private static void ConfigureEfCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseSqlServer(
                configuration.GetConnectionString(Constants.DefaultConnection)
            )
        );
    }
    private static void ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentity<Customer, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;
        }).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();
    }
    private static void ConfigureAuthenticationSchema(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidAudience = configuration[Constants.Audience],
                ValidIssuer = configuration[Constants.Issuer],
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[Constants.Key]!)),
                ValidateIssuerSigningKey = true
            };
        });
    }
    private static void ConfigureCors(this IServiceCollection services)
        => services.AddCors(option =>
        {
            option.AddPolicy(Constants.AppPolicy, builder =>
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
        });
    public static void ConfigureAllRequiredServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureEfCore(configuration);
        services.ConfigureIdentity();
        services.ConfigureCors();
        services.ConfigureAuthenticationSchema(configuration);
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IReviewRepository, ReviewRepository>();
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<IReviewService, ReviewService>();
        // this to configure the IMiddleware interface
        services.AddTransient<GlobalErrorHandlerMiddleware>();
    }
}
