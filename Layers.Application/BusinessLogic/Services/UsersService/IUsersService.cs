using Layers.Application.Requests;

namespace Layers.Application.BusinessLogic.Services.UsersService;

public interface IUsersService
{
    /// <summary>
    /// Регистрирует пользователя и заносит его в БД
    /// </summary>
    /// <param name="request"> Запрос на регистрацию </param>
    /// <param name="cancellationToken"> Токен отмены</param>
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Возвращает идентификатор пользователя, пытающегося войти в аккаунт
    /// </summary>
    /// <param name="request"> Запрос на аутентификацию </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    Task<Guid?> GetUserIdForLoginAsync(LoginRequest request, CancellationToken cancellationToken);
}