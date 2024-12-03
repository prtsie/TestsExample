using System.Security.Claims;
using Layers.Application.BusinessLogic.Services.UsersService;
using Layers.Application.BusinessLogic.Services.UsersService.Exceptions;
using Layers.Application.Requests;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace TestsExample.Controllers;

public class UserController : Controller
{
    private readonly IUsersService usersService;

    public UserController(IUsersService usersService)
    {
        this.usersService = usersService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        try
        {
            await usersService.RegisterAsync(request, cancellationToken);
        }
        catch (UserAlreadyExistsException)
        {
            return View(request);
        }
        
        return RedirectToAction(nameof(Login));
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken,
        [FromQuery] string returnUrl = "/")
    {
        
        try
        {
            var userId = await usersService.GetUserIdForLoginAsync(request, cancellationToken);
            
            var claims = new Claim[]
            {
                new(ClaimTypes.Name, request.Name),
                new(ClaimTypes.NameIdentifier, userId.ToString()!)
            };
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(identity.AuthenticationType, new(identity));

            return LocalRedirect(returnUrl);
        }
        catch (InvalidCredentialsException)
        {
            return View(request);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string returnUrl = "/")
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect(returnUrl);
    }
}