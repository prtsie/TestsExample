using Microsoft.EntityFrameworkCore;
using TestsExample.Database.Context;
using TestsExample.Database.Models;

namespace TestsExample.Database.Repositories.Generic;

/// <summary>
/// <inheritdoc cref="IGenericRepository{T}"/>
/// </summary>
public abstract class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
{
    private readonly IGenericReader reader;
    private readonly IGenericWriter writer;

    protected GenericRepository(IGenericReader reader, IGenericWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }

    async Task<T[]> IGenericRepository<T>.GetAllAsync(CancellationToken cancellationToken)
        => await reader.Read<T>().ToArrayAsync(cancellationToken);

    async Task<T?> IGenericRepository<T>.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await reader.Read<T>().SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public void Add(T entity)
        => writer.Add(entity);

    /// <inheritdoc />
    public void Update(T entity)
    {
        writer.Update(entity);
    }

    /// <inheritdoc />
    public void Remove(T entity)
    {
        writer.Remove(entity);
    }
    
    /// <summary> Получает все записи из БД в виде запроса <see cref="IQueryable{T}"/> </summary>
    /// <returns> IQueryable-запрос со всеми сущностями без отслеживания изменений </returns>
    protected IQueryable<T> Read() => reader.Read<T>().AsNoTracking();
}