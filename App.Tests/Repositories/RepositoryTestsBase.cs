using Layers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Repositories.Tests.Repositories;

/// <summary>
/// Базовый класс для тестов на репозитории
/// </summary>
public class RepositoryTestsBase
{
    /// <summary>
    /// Контекст БД в оперативной памяти для тестирования 
    /// </summary>
    protected readonly TestsSampleDbContext Context;
    
    public RepositoryTestsBase()
    {
        var opts = new DbContextOptionsBuilder<TestsSampleDbContext>()
            // Добавляем к названию БД случайный идентификатор, чтобы при параллельном выполнении тесты не мешали друг другу
            .UseInMemoryDatabase($"TestDB{Guid.NewGuid()}")
            // БД на памяти не поддерживает транзакции, так что можно отключить предупреждение
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        Context = new(opts);
    }
}