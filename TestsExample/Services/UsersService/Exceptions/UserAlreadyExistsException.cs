using TestsExample.Exceptions.Common;
using TestsExample.Requests;

namespace TestsExample.Services.UsersService.Exceptions;

public class UserAlreadyExistsException : TestsExampleExceptionBase
{
    public UserAlreadyExistsException(RegisterRequest request) : base($"Пользователь с именем '{request.Name}' уже существует.") { }
}