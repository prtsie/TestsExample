using Layers.Application.Exceptions.Common;

namespace Layers.Application.BusinessLogic.Services.UsersService.Exceptions;

public class InvalidCredentialsException : TestsExampleExceptionBase
{
    public InvalidCredentialsException() : base("Неверный логин или пароль") { }
}