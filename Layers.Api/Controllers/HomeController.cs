using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Layers.Application.Models;
using Layers.Application.Requests;
using Layers.Application.Services.Posts;
using Microsoft.AspNetCore.Authorization;
using TestsExample.Helpers.Common;
using TestsExample.Models;
using TestsExample.Services.IdentityProvider;

namespace TestsExample.Controllers;

public class HomeController : Controller
{
    private readonly IPostService postService;
    private readonly IIdentityProvider identityProvider;

    public HomeController(IPostService postService, IIdentityProvider identityProvider)
    {
        this.postService = postService;
        this.identityProvider = identityProvider;
    }

    public async Task<IActionResult> TheWall(CancellationToken cancellationToken, Sort sort = Sort.Date)
    {
        var posts = await postService.GetPostsAsync(sort, cancellationToken);

        return View(posts);
    }
    
    [Authorize]
    public IActionResult CreatePost()
    {
        return View();
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePost(CreatePostRequest createPostRequest,CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(createPostRequest);
        }
        await postService.CreatePostAsync(createPostRequest, identityProvider.User!.GetId()!.Value, cancellationToken);
        return LocalRedirect("/");
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> DeletePost(Guid postId,CancellationToken cancellationToken)
    {
        await postService.DeletePostAsync(postId, identityProvider.User!.GetId()!.Value, cancellationToken);
        return LocalRedirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}