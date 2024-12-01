using TestsExample.Database.Models;
using TestsExample.Database.Repositories.Generic;

namespace TestsExample.Database.Repositories.PostRepository;

/// <summary>
/// Репозиторий для доступа к постам в БД
/// </summary>
public interface IPostsRepository : IGenericRepository<Post> { }