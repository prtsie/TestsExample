using Layers.Application.Services.PostsService;
using Layers.Application.Services.UsersService;
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