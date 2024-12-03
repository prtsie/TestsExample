using TestsExample.Database.Context;
using TestsExample.Database.Exceptions;
using TestsExample.Database.Models;
using TestsExample.Database.Repositories.PostRepository;
using TestsExample.Database.Repositories.UserRepository;
using TestsExample.Exceptions.Auth;
using TestsExample.Helpers;
using TestsExample.MappingExtensions;
using TestsExample.Models;
using TestsExample.Requests;
using TestsExample.Services.IdentityProvider;

namespace TestsExample.Services.PostsService;

public class PostsService : IPostsService
{
    private readonly IPostsRepository postsRepository;
    private readonly IUsersRepository usersRepository;
    private readonly IUnitOfWork unitOfWork;
    private readonly IIdentityProvider identityProvider;

    public PostsService(
        IPostsRepository postsRepository,
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork,
        IIdentityProvider identityProvider)
    {
        this.postsRepository = postsRepository;
        this.usersRepository = usersRepository;
        this.unitOfWork = unitOfWork;
        this.identityProvider = identityProvider;
    }

    /// <summary> Достаёт текущего пользователя из контекста запроса </summary>
    private async Task<User> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userName = identityProvider.User?.GetName() ?? throw new NotAuthorizedException();
        var user = await usersRepository.GetByNameAsync(userName, cancellationToken) ?? throw new NotAuthorizedException();
        return user;
    }

    async Task<IEnumerable<PostViewModel>> IPostsService.GetPostsAsync(CancellationToken cancellationToken)
    {
        var posts = await postsRepository.GetAllAsync(cancellationToken);

        var models = posts.Select(p =>
        {
            var author = usersRepository.GetByIdAsync(p.UserId, cancellationToken).Result;

            return p.MapToPostViewModel(
                author!.Name //TODO: обработка null
                );
        });
        
        return models;
    }

    async Task IPostsService.CreatePostAsync(CreatePostRequest request, CancellationToken cancellationToken)
    {
        var author = await GetCurrentUser(cancellationToken);

        var post = request.MapToPost(author.Id);
        postsRepository.Add(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IPostsService.DeletePostAsync(Guid postId, CancellationToken cancellationToken)
    {
        var user = await GetCurrentUser(cancellationToken);
        
        var post = await postsRepository.GetByIdAsync(postId, cancellationToken) 
                   ?? throw new EntityNotFoundByIdException<Post>(postId);
        
        if (post.UserId != user.Id)
        {
            throw new NotAuthorizedException();
        }
        
        postsRepository.Remove(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}