using TestsExample.Models;

namespace Layers.Application.NeededServices.Database.Repositories;

/// <summary>
/// Репозиторий для доступа к постам в БД
/// </summary>
public interface IPostRepository : IGenericRepository<Post> { }