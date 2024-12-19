using Layers.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Repositories.Tests.Repositories;

public class RepositoryTestsBase
{
    protected readonly TestsSampleDbContext Context;
    
    public RepositoryTestsBase()
    {
        var opts = new DbContextOptionsBuilder<TestsSampleDbContext>()
            .UseInMemoryDatabase("TestDB")
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        Context = new(opts);
        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }
}