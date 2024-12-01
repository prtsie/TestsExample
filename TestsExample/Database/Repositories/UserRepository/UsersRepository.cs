using Microsoft.EntityFrameworkCore;
using TestsExample.Database.Context;
using TestsExample.Database.Models;
using TestsExample.Database.Repositories.Generic;

namespace TestsExample.Database.Repositories.UserRepository;

/// <summary> <inheritdoc cref="IUsersRepository"/> </summary>
public class UsersRepository : GenericRepository<User>, IUsersRepository
{
    public UsersRepository(IGenericReader reader, IGenericWriter writer) : base(reader, writer) { }

    /// <summary> Найти пользователя в БД по имени </summary>
    /// <param name="name"> Имя пользователя</param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Пользователя или null, если пользователь не найден </returns>
    public Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken)
        => Read().SingleOrDefaultAsync(u => u.Name == name, cancellationToken);
}