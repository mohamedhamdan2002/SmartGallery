using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmartGallery.Server.Models;
using SmartGallery.Server.Repositories;
using SmartGallery.Server.Repositories.Contracts;
using SmartGallery.Server.Services;

namespace SmartGallery.Server;
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
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<AppDbContext>()
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
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = configuration["AuthSettings:Audience"],
                ValidIssuer = configuration["AuthSettings:Issuer"],
                RequireExpirationTime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"])),
                ValidateIssuerSigningKey = true
            };
        });
    }
    private static void ConfigureUserService(this IServiceCollection services)
        => services.AddScoped<IUserService, UserService>();
    private static void ConfigureIRepositoryManager(this IServiceCollection services)
            => services.AddScoped<IRepositoryManager, RepositoryManager>();
    public static void ConfigureAllRequiredServices(this IServiceCollection services, IConfiguration configuration) 
    {
        services.ConfigureEfCore(configuration);
        services.ConfigureIdentity();
        services.ConfigureAuthenticationSchema(configuration);
        services.ConfigureUserService();
        services.ConfigureIRepositoryManager();
    }
}
