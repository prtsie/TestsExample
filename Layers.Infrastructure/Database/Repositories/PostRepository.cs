using Layers.Application.Models;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.DbModels;
using Layers.Infrastructure.DbModels.MappingExtensions;
using Microsoft.EntityFrameworkCore;

namespace Layers.Infrastructure.Database.Repositories;

/// <summary> <inheritdoc cref="IPostRepository"/> </summary>
public class PostRepository : IPostRepository
{
    private readonly IGenericReader reader;
    private readonly IGenericWriter writer;

    public PostRepository(IGenericReader reader, IGenericWriter writer)
    {
        this.reader = reader;
        this.writer = writer;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<TestsExample.Models.Post>> Get(Sort sort, CancellationToken cancellationToken)
    {
        var result = await GetIQueryable(sort).ToArrayAsync(cancellationToken);
        return result.Select(p => p.ToDomainPost());
    }

    /// <summary>
    /// Возвращает IQueryable, который является ещё не выполненным запросом, но имеющим нужные фильтры и сортировки
    /// </summary>
    /// <returns> БД-модель постов </returns>
    /// <remarks> Нужен для того, чтобы не писать несколько раз одно и то же </remarks>
    private IQueryable<Post> GetIQueryable(Sort sort)
    {
        var posts = reader.Read<Post>();
        var sorted = sort switch
        {
            Sort.Date => posts.OrderByDescending(p => p.PublicationDateTime),
            Sort.DateReverse => posts.OrderBy(p => p.PublicationDateTime),
            Sort.Author => posts.OrderBy(p => p.User.Name),
            Sort.AuthorReverse => posts.OrderByDescending(p => p.User.Name),
            Sort.Title => posts.OrderBy(p => p.Title),
            Sort.TitleReverse => posts.OrderByDescending(p => p.Title),
            _ => throw new ArgumentOutOfRangeException(nameof(sort), sort, null)
        };

        return sorted;
    }

    /// <inheritdoc />
    public async Task<TestsExample.Models.Post?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await reader.Read<Post>().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);
        return result?.ToDomainPost();
    }


    /// <inheritdoc />
    public async Task<IEnumerable<Tuple<TestsExample.Models.Post, string>>> GetWithAuthorName(
        Sort sort,
        CancellationToken cancellationToken)
    {
        var result = await GetIQueryable(sort)
            .Select(p => new Tuple<TestsExample.Models.Post, string>(p.ToDomainPost(), p.User.Name))
            .ToArrayAsync(cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public void Add(TestsExample.Models.Post post)
    {
        writer.Add(post.ToDbPost());
    }

    /// <inheritdoc />
    public void Update(TestsExample.Models.Post post)
    {
        writer.Update(post.ToDbPost());
    }

    /// <inheritdoc />
    public void Delete(TestsExample.Models.Post post)
    {
        writer.Remove<Post>(post.Id);
    }
}