using Layers.Application.Models;
using TestsExample.Models;

namespace Layers.Application.NeededServices.Database.Repositories;

/// <summary>
/// Репозиторий для доступа к постам в БД
/// </summary>
public interface IPostRepository
{
    /// <summary>
    /// Получить отсортированные посты из хранилища
    /// </summary>
    /// <param name="sort"> Вариант сортировки </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Коллекцию постов </returns>
    Task<IEnumerable<Post>> Get(Sort sort, CancellationToken cancellationToken);

    /// <summary>
    /// Получить пост по идентификатору
    /// </summary>
    /// <param name="id"> Идентификатор </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    /// <returns> Найденный пост или null </returns>
    Task<Post?> GetById(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// Добавить пост в хранилище
    /// </summary>
    /// <param name="post"> Пост для добавления </param>
    void Add(Post post);

    /// <summary>
    /// Обновить пост в хранилище
    /// </summary>
    /// <param name="post"> Пост для обновления </param>
    void Update(Post post);

    /// <summary>
    /// Удалить пост из хранилища
    /// </summary>
    /// <param name="post"> Пост для удаления</param>
    void Delete(Post post);
}