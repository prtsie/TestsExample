using TestsExample.Database.Models;

namespace TestsExample.Database.Repositories.Generic;

/// <summary>
/// Паттерн "Репозиторий", используемый, чтобы сделать доступ к БД более структурированным
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public interface IGenericRepository<T> where T : EntityBase
{
    /// <summary> Получить все сущности из хранилища </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<T[]> GetAllAsync(CancellationToken cancellationToken);

    ///<summary> Получить одну сущность по идентификатору </summary>
    /// <param name="id">Идентификатор сущности</param>
    /// <param name="cancellationToken">Токен отмены</param>
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary> Добавить сущность в хранилище </summary>
    /// <param name="entity">Сущность для добавления</param>
    void Add(T entity);

    /// <summary> Обновить сущность в хранилище </summary>
    /// <param name="entity">Сущность для обновления</param>
    void Update(T entity);

    ///<summary> Удалить сущность из хранилища </summary>
    /// <param name="entity">Сущность для удаления</param>
    void Remove(T entity);
}