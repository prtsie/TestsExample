using System.Security.Claims;

namespace TestsExample.Helpers;

 static class ClaimsPrincipalExtensions
{
    public static string? GetName(this ClaimsPrincipal user)
     => user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
}