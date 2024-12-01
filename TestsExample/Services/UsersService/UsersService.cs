using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TestsExample.Database.Context;
using TestsExample.Database.Models;
using TestsExample.Database.Repositories.UserRepository;
using TestsExample.Requests;
using TestsExample.Services.UsersService.Exceptions;

namespace TestsExample.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly IUsersRepository usersesRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IHttpContextAccessor httpContextAccessor;

    public UsersService(
        IUsersRepository usersesRepository,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor)
    {
        this.usersesRepository = usersesRepository;
        this.unitOfWork = unitOfWork;
        this.httpContextAccessor = httpContextAccessor;
    }

    async Task IUsersService.RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var foundUser = await usersesRepository.GetByNameAsync(request.Name, cancellationToken);
        if (foundUser is not null)
        {
            throw new UserAlreadyExistsException(request);
        }

        var user = new User
        {
            Name = request.Name,
            Password = request.Password
        };

        usersesRepository.Add(user);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IUsersService.LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await usersesRepository.GetByNameAsync(request.Name, cancellationToken);
        if (user is null || user.Password != request.Password)
        {
            throw new InvalidCredentialsException();
        }
        
        var claims = new Claim[] { new(ClaimTypes.Name, request.Name) };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

        await httpContextAccessor.HttpContext!.SignInAsync(identity.AuthenticationType, new(identity));
    }
}