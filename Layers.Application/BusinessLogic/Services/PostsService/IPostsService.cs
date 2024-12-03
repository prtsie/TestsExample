using Layers.Application.Models;
using Layers.Application.Requests;

namespace Layers.Application.BusinessLogic.Services.PostsService;

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
    /// <param name="request"> Запрос на создание поста </param>
    /// <param name="userId"> Идентификатор пользователя, отправившего запрос на создание </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    Task CreatePostAsync(CreatePostRequest request, Guid userId, CancellationToken cancellationToken);

    /// <summary>
    /// Удаляет пост из БД
    /// </summary>
    /// <param name="postId"> Идентификатор поста </param>
    /// <param name="userId"> Идентификатор пользователя, отправившего запрос на удаление </param>
    /// <param name="cancellationToken"> Токен отмены</param>
    Task DeletePostAsync(Guid postId, Guid userId, CancellationToken cancellationToken);
}