﻿using System.Reflection;
using Layers.Application.NeededServices.Database;
using Microsoft.EntityFrameworkCore;

namespace Layers.Infrastructure.Database;

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

    void IGenericWriter.Remove<T, TKey>(TKey key)
    {
        var tracked = Find<T>(key);
        Remove(tracked!);
    }

    public void Remove<T>(Guid key) where T : class
    {
        var tracked = Find<T>(key);
        Remove(tracked!);
    }

    IQueryable<T> IGenericReader.Read<T>() => Set<T>();

    Task IUnitOfWork.SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(cancellationToken);
}