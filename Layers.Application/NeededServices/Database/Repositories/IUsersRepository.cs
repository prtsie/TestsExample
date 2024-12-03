using TestsExample.Models;

namespace Layers.Application.NeededServices.Database.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями в БД
/// </summary>
public interface IUsersRepository : IGenericRepository<User>
{
    /// <summary> Найти пользователя в БД по имени </summary>
    /// <param name="name"> Имя пользователя</param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Пользователя или null, если пользователь не найден </returns>
    Task<User?> GetByNameAsync(string name, CancellationToken cancellationToken);
}