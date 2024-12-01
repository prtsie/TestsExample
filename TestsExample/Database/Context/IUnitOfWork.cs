namespace TestsExample.Database.Context;

/// <summary>
/// Паттерн "единица работы", используется для управления транзакциями и сохранением изменений в БД.
/// Любые изменения сохранятся в БД только при вызове метода SaveChangesAsync() этого интерфейса.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Сохранить все изменения в БД
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены</param>
    Task SaveChangesAsync(CancellationToken cancellationToken);
}