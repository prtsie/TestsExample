using Layers.Application.Services.Posts;
using Layers.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Layers.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Подключает сервисы с бизнес-логикой
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        
        return services;
    }
}