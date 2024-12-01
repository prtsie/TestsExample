using TestsExample.Requests;

namespace TestsExample.Services.UsersService;

public interface IUsersService
{
    /// <summary>
    /// Регистрирует пользователя и заносит его в БД
    /// </summary>
    /// <param name="request"> Запрос пользователя на регистрацию </param>
    /// <param name="cancellationToken"> Токен отмены</param>
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Аутентифицирует пользователя
    /// </summary>
    /// <param name="request"> Запрос на аутентификацию </param>
    /// <param name="cancellationToken"> Токен отмены </param>
    Task LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}