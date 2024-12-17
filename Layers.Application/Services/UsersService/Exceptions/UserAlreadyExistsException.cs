using Layers.Application.Exceptions.Common;
using Layers.Application.Requests;

namespace Layers.Application.Services.UsersService.Exceptions;

public class UserAlreadyExistsException : TestsExampleExceptionBase
{
    public UserAlreadyExistsException(RegisterRequest request) : base($"Пользователь с именем '{request.Name}' уже существует.") { }
}