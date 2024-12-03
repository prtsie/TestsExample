using TestsExample.Models;

namespace Layers.Application.NeededServices.Database;

/// <summary>
/// Записывает в базу данных
/// </summary>
public interface IGenericWriter
{
    /// <summary> Добавить новую сущность в хранилище </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <typeparam name="T">Тип сущности</typeparam>
    void Add<T>(T entity) where T : EntityBase;

    ///<summary> Обновить сущность в хранилище </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <typeparam name="T">Тип сущности</typeparam>
    void Update<T>(T entity) where T : EntityBase;

    ///<summary> Удалить сущность из хранилища </summary>
    /// <param name="entity">Сущность для удаления</param>
    /// <typeparam name="T">Тип сущности</typeparam>
    void Remove<T>(T entity) where T : EntityBase;
}