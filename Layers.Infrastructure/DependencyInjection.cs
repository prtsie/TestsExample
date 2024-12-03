using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.Database;
using Layers.Infrastructure.Database.Repositories.PostRepository;
using Layers.Infrastructure.Database.Repositories.UserRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Layers.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Подключает инфраструктурные сервисы, такие как база данных
    /// </summary>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfigurationRoot configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<TestsSampleDbContext>(optsBuilder => optsBuilder.UseSqlServer(connectionString));
        
        services.AddScoped<IGenericReader>(provider => provider.GetRequiredService<TestsSampleDbContext>());
        services.AddScoped<IGenericWriter>(provider => provider.GetRequiredService<TestsSampleDbContext>());
        
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TestsSampleDbContext>());
        
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        
        return services;
    }
}