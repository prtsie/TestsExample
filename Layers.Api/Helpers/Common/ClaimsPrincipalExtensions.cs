using System.Security.Claims;

namespace TestsExample.Helpers.Common;

public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Возвращает имя пользователя, если он авторизован
    /// </summary>
    /// <returns> Строка с именем или null </returns>
    public static string? GetName(this ClaimsPrincipal user)
     => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
    
    /// <summary>
    /// Возвращает идентификатор пользователя, если он авторизован
    /// </summary>
    /// <returns> Guid или null</returns>
    public static Guid? GetId(this ClaimsPrincipal user)
    {
        var str = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        return str is null ? null : Guid.Parse(str);
    }
}