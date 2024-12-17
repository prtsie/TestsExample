using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using TestsExample.Models;

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
    public async Task<User?> GetByName(string name, CancellationToken cancellationToken)
        => await reader.Read<User>().SingleOrDefaultAsync(u => u.Name == name, cancellationToken);

    /// <inheritdoc />
    public async Task<User?> GetById(Guid id, CancellationToken cancellationToken)
        => await reader.Read<User>().SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    public void Add(User user) => writer.Add(user);
}