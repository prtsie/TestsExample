using System.Security.Claims;

namespace TestsExample.Services.IdentityProvider;

/// <summary>
/// Сервис для доступа к текущему пользователю
/// </summary>
public interface IIdentityProvider
{
    /// <summary> Текущий пользователь </summary>
    public ClaimsPrincipal? User { get; }
}