using Layers.Application.BusinessLogic.Services.UsersService.Exceptions;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Application.Requests;
using TestsExample.Models;

namespace Layers.Application.BusinessLogic.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly IUsersRepository usersRepository;
    private readonly IUnitOfWork unitOfWork;

    public UsersService(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        this.usersRepository = usersRepository;
        this.unitOfWork = unitOfWork;
    }

    async Task IUsersService.RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var foundUser = await usersRepository.GetByNameAsync(request.Name, cancellationToken);
        if (foundUser is not null)
        {
            throw new UserAlreadyExistsException(request);
        }

        var user = new User
        {
            Name = request.Name,
            Password = request.Password
        };

        usersRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task<Guid?> IUsersService.GetUserIdForLoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByNameAsync(request.Name, cancellationToken);
        if (user is null || user.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }
        
        return user.Id;
    }
}