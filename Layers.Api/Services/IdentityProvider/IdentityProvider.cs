using System.Security.Claims;

namespace TestsExample.Services.IdentityProvider;

/// <inheritdoc />
public class IdentityProvider : IIdentityProvider
{
    private readonly IHttpContextAccessor httpContextAccessor; //todo перенести класс куданить

    public IdentityProvider(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    /// <inheritdoc />
    public ClaimsPrincipal? User => httpContextAccessor.HttpContext?.User;
}