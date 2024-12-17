using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Application.Requests;
using Layers.Application.Services.Users.Exceptions;
using TestsExample.Models;

namespace Layers.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    public UserService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    async Task IUserService.RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var foundUser = await userRepository.GetByNameAsync(request.Name, cancellationToken);
        if (foundUser is not null)
        {
            throw new UserAlreadyExistsException(request);
        }

        var user = new User
        {
            Name = request.Name,
            Password = request.Password
        };

        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task<Guid?> IUserService.GetUserIdForLoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByNameAsync(request.Name, cancellationToken);
        if (user is null || user.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }
        
        return user.Id;
    }
}