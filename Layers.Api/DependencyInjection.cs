using Layers.Application;
using Layers.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestsExample.Services.IdentityProvider;

namespace TestsExample;

public static class DependencyInjection
{
    /// <summary>
    /// Подключает сервисы всех слоёв
    /// </summary>
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddPresentation();
    }
    
    /// <summary>
    /// Подключает сервисы для обработки пользовательских запросов
    /// </summary>
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddScoped<IIdentityProvider, IdentityProvider>();
        
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/User/Login";
                options.AccessDeniedPath = options.LoginPath;
                options.LogoutPath = "/User/Logout";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

        services.AddHttpContextAccessor();

        services.AddControllersWithViews();
        
        return services;
    }
}