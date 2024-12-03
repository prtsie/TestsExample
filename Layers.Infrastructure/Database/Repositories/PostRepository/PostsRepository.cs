using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.Database.Repositories.Generic;
using TestsExample.Models;

namespace Layers.Infrastructure.Database.Repositories.PostRepository;

/// <summary> <inheritdoc cref="IPostsRepository"/> </summary>
public class PostsRepository : GenericRepository<Post>,  IPostsRepository
{
    public PostsRepository(IGenericReader reader, IGenericWriter writer) : base(reader, writer) { }
}