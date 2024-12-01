using TestsExample.Exceptions.Common;

namespace TestsExample.Services.UsersService.Exceptions;

public class InvalidCredentialsException : TestsExampleExceptionBase
{
    public InvalidCredentialsException() : base("Неверный логин или пароль") { }
}