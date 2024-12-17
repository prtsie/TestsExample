using System.Security.Claims;
using Layers.Application.Requests;
using Layers.Application.Services.UsersService;
using Layers.Application.Services.UsersService.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace TestsExample.Controllers;

public class UserController : Controller
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
    {
        this.userService = userService;
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
            await userService.RegisterAsync(request, cancellationToken);
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
            var userId = await userService.GetUserIdForLoginAsync(request, cancellationToken);
            
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