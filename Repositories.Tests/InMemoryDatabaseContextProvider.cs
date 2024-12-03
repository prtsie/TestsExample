using Layers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Repositories.Tests;

public class InMemoryDatabaseContextProvider
{
    private static readonly DbContextOptions<TestsSampleDbContext> Opts = new DbContextOptionsBuilder<TestsSampleDbContext>()
        .UseInMemoryDatabase("committeeTestDB")
        .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
        .Options;

    public static TestsSampleDbContext GetDbContext()
    {
        var result = new TestsSampleDbContext(Opts);
        return result;
    }
}