using Ahatornn.TestGenerator;
using FluentAssertions;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.Database;
using Layers.Infrastructure.Database.Repositories.UserRepository;
using TestsExample.Models;
using Xunit;

namespace Repositories.Tests.Repositories;

public class GenericRepositoryTests
{
    private readonly TestsSampleDbContext context = InMemoryDatabaseContextProvider.GetDbContext();
    private readonly IGenericRepository<User> repository;

    public GenericRepositoryTests()
    {
        repository = new UsersRepository(context, context);
    }

    [Fact]
    private async Task GetShouldReturnEntities()
    {
        var user1 = TestEntityProvider.Shared.Create<User>();
        var user2 = TestEntityProvider.Shared.Create<User>();
        context.AddRange(user1, user2);
        await context.SaveChangesAsync();

        var users = await repository.GetAllAsync(CancellationToken.None);
        
        users.Should().BeEquivalentTo(new[] { user1, user2 });
    }

    [Fact]
    private async Task GetShouldReturnEmpty()
    {
        var users = await repository.GetAllAsync(CancellationToken.None);

        users.Should().BeEmpty();
    }
}