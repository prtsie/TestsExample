using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.Database.Repositories.Generic;
using TestsExample.Models;

namespace Layers.Infrastructure.Database.Repositories;

/// <summary> <inheritdoc cref="IPostRepository"/> </summary>
public class PostRepository : GenericRepository<Post>,  IPostRepository
{
    public PostRepository(IGenericReader reader, IGenericWriter writer) : base(reader, writer) { }
}