using Layers.Application.Exceptions.Common;

namespace Layers.Application.Exceptions.Auth;

public class NotAuthorizedException : TestsExampleExceptionBase
{
    public NotAuthorizedException() : base("Вы не авторизованы для выполнения этого действия") { }
}