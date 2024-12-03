using Layers.Application.BusinessLogic.Services.PostsService;
using Layers.Application.BusinessLogic.Services.UsersService;
using Microsoft.Extensions.DependencyInjection;

namespace Layers.Application;

public static class DependencyInjection
{
    /// <summary>
    /// Подключает сервисы с бизнес-логикой
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IUsersService, UsersService>();
        services.AddScoped<IPostsService, PostsService>();
        
        return services;
    }
}