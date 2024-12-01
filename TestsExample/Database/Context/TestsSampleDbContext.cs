using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace TestsExample.Database.Context;

public sealed class TestsSampleDbContext : DbContext, IGenericWriter, IGenericReader, IUnitOfWork
{
    public TestsSampleDbContext(DbContextOptions<TestsSampleDbContext> opts) : base(opts)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    void IGenericWriter.Add<T>(T entity) => Add(entity);

    void IGenericWriter.Update<T>(T entity) => Update(entity);

    void IGenericWriter.Remove<T>(T entity) => Remove(entity);

    IQueryable<T> IGenericReader.Read<T>() => Set<T>();

    Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
}