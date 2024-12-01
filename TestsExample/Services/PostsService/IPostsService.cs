using TestsExample.Models;
using TestsExample.Requests;

namespace TestsExample.Services.PostsService;

public interface IPostsService
{
    /// <summary>
    /// Возвращает посты из БД
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены</param>
    /// <returns> <see cref="IEnumerable{T}"/> </returns>
    Task<IEnumerable<PostViewModel>> GetPostsAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Создаёт и добавляет пост в БД
    /// </summary>
    /// <param name="createPostRequest"> Запрос на создание поста </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    Task CreatePostAsync(CreatePostRequest createPostRequest, CancellationToken cancellationToken);
}