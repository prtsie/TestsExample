using Layers.Application.Exceptions.Common;

namespace Layers.Application.Services.UsersService.Exceptions;

public class InvalidCredentialsException : TestsExampleExceptionBase
{
    public InvalidCredentialsException() : base("Неверный логин или пароль") { }
}