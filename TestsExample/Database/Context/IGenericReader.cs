using TestsExample.Database.Models;

namespace TestsExample.Database.Context;

/// <summary>
/// Читает из базы данных
/// </summary>
public interface IGenericReader
{
    /// Возвращает все сущности указанного типа из базы данных
    /// <typeparam name="T">Тип сущности</typeparam>
    IQueryable<T> Read<T>() where T : EntityBase;
}