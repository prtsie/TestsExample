namespace Layers.Application.NeededServices.Database;

/// <summary>
/// Записывает в базу данных
/// </summary>
public interface IGenericWriter
{
    /// <summary> Добавить новую сущность в хранилище </summary>
    /// <param name="entity">Сущность для добавления</param>
    /// <typeparam name="T">Тип сущности</typeparam>
    void Add<T>(T entity) where T : class;

    ///<summary> Обновить сущность в хранилище </summary>
    /// <param name="entity">Сущность для обновления</param>
    /// <typeparam name="T">Тип сущности</typeparam>
    void Update<T>(T entity) where T : class;

    /// <summary> Удалить сущность из хранилища </summary>
    /// <param name="key"> Первичный ключ </param>
    /// <typeparam name="T"> Тип сущности </typeparam>
    /// <typeparam name="TKey"> Тип ключа </typeparam>
    void Remove<T, TKey>(TKey key) where T : class;

    /// <summary> Удалить сущность из хранилища </summary>
    /// <param name="key"> Первичный ключ </param>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <remarks> Перегрузка для сущностей с не составным первичным ключом типа <see cref="Guid"/> </remarks>
    void Remove<T>(Guid key) where T : class;
}