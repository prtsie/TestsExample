using TestsExample.Exceptions.Common;

namespace TestsExample.Exceptions.Auth;

public class NotAuthorizedException : TestsExampleExceptionBase
{
    public NotAuthorizedException() : base("Вы не авторизованы для выполнения этого действия") { }
}