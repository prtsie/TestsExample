using Layers.Application.BusinessLogic.Services.UsersService.Exceptions;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Application.Requests;
using TestsExample.Models;

namespace Layers.Application.BusinessLogic.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    public UsersService(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    async Task IUsersService.RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
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

    async Task<Guid?> IUsersService.GetUserIdForLoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByNameAsync(request.Name, cancellationToken);
        if (user is null || user.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }
        
        return user.Id;
    }
}