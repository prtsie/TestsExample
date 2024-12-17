using TestsExample.Models;

namespace Layers.Application.NeededServices.Database.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями в БД
/// </summary>
public interface IUserRepository
{
    /// <summary> Найти пользователя в хранилище по имени </summary>
    /// <param name="name"> Имя пользователя</param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Пользователя или null, если пользователь не найден </returns>
    Task<User?> GetByName(string name, CancellationToken cancellationToken);

    /// <summary> Найти пользователя в хранилище по идентификатору </summary>
    /// <param name="id"> Идентификатор </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Найденного пользователя или null</returns>
    Task<User?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить пользователя в хранилище
    /// </summary>
    /// <param name="user"> Пользователь </param>
    void Add(User user);
}