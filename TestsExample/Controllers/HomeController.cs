using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestsExample.Models;
using TestsExample.Requests;
using TestsExample.Services.PostsService;

namespace TestsExample.Controllers;

public class HomeController : Controller
{
    private readonly IPostsService postsService;

    public HomeController(IPostsService postsService)
    {
        this.postsService = postsService;
    }

    public async Task<IActionResult> TheWall(CancellationToken cancellationToken)
    {
        var posts = await postsService.GetPostsAsync(cancellationToken);

        return View(posts);
    }
    
    public IActionResult CreatePost()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CreatePost(CreatePostRequest createPostRequest,CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(createPostRequest);
        }
        await postsService.CreatePostAsync(createPostRequest, cancellationToken);
        return LocalRedirect("/");
    }
    
    [HttpPost]
    public async Task<IActionResult> DeletePost(Guid postId,CancellationToken cancellationToken)
    {
        await postsService.DeletePostAsync(postId, cancellationToken);
        return LocalRedirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}