using TestsExample.Database.Context;
using TestsExample.Database.Models;
using TestsExample.Database.Repositories.Generic;

namespace TestsExample.Database.Repositories.PostRepository;

/// <summary> <inheritdoc cref="IPostsRepository"/> </summary>
public class PostsRepository : GenericRepository<Post>,  IPostsRepository
{
    public PostsRepository(IGenericReader reader, IGenericWriter writer) : base(reader, writer) { }
}