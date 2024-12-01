using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using TestsExample.Requests;
using TestsExample.Services.UsersService;
using TestsExample.Services.UsersService.Exceptions;

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
            await usersService.LoginAsync(request, cancellationToken);
        }
        catch (InvalidCredentialsException)
        {
            return View(request);
        }

        return LocalRedirect(returnUrl);
    }

    [HttpGet]
    public async Task<IActionResult> Logout(string returnUrl = "/")
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect(returnUrl);
    }
}