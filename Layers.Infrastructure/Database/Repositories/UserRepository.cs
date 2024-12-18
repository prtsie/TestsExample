using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.DbModels;
using Layers.Infrastructure.DbModels.MappingExtensions;
using Microsoft.EntityFrameworkCore;

namespace Layers.Infrastructure.Database.Repositories;

/// <summary> <inheritdoc cref="IUserRepository"/> </summary>
public class UserRepository : IUserRepository
{
    private readonly IGenericReader reader;
    private readonly IGenericWriter writer;

    public UserRepository(IGenericReader reader, IGenericWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }

    /// <inheritdoc />
    public async Task<TestsExample.Models.User?> GetByName(string name, CancellationToken cancellationToken)
    {
        var result = await reader.Read<User>().SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
        return result?.ToDomainUser();
    }

    /// <inheritdoc />
    public async Task<TestsExample.Models.User?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await reader.Read<User>().SingleOrDefaultAsync(u => u.Id == id, cancellationToken);
        return result?.ToDomainUser();
    }
    
    /// <inheritdoc />
    public void Add(TestsExample.Models.User user)
    {
        writer.Add(user.ToDbUser());
    }
}