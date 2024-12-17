using Layers.Application.Models;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using TestsExample.Models;

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
    public async Task<IEnumerable<Post>> Get(Sort sort, CancellationToken cancellationToken)
    {
        var posts = reader.Read<Post>();
        var sorted = sort switch
        {
            Sort.Date => posts.OrderBy(p => p.PublicationDateTime),
            Sort.DateReverse => posts.OrderByDescending(p => p.PublicationDateTime),
            Sort.Author => posts
                .Join(reader.Read<User>(), p => p.UserId, u => u.Id, (p, u) => new Tuple<Post, string>(p, u.Name))
                .OrderBy(tuple => tuple.Item2)
                .Select(tuple => tuple.Item1),
            Sort.AuthorReverse => posts
                .Join(reader.Read<User>(), p => p.UserId, u => u.Id, (p, u) => new Tuple<Post, string>(p, u.Name))
                .OrderByDescending(tuple => tuple.Item2)
                .Select(tuple => tuple.Item1),
            Sort.Title => posts.OrderBy(p => p.Title),
            Sort.TitleReverse => posts.OrderByDescending(p => p.Title),
            _ => throw new ArgumentOutOfRangeException(nameof(sort), sort, null)
        };

        return await sorted.ToArrayAsync(cancellationToken);
    }

    public async Task<Post?> GetById(Guid id, CancellationToken cancellationToken)
        => await reader.Read<Post>().SingleOrDefaultAsync(p => p.Id == id, cancellationToken);

    /// <inheritdoc />
    public void Add(Post post) => writer.Add(post);

    /// <inheritdoc />
    public void Update(Post post) => writer.Update(post);

    /// <inheritdoc />
    public void Delete(Post post) => writer.Remove(post);
}