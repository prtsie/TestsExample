using TestsExample.Database.Context;
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

    /// <summary>
    /// Возвращает посты из БД
    /// </summary>
    /// <param name="cancellationToken"> Токен отмены</param>
    /// <returns> Массив <see cref="PostViewModel"/> </returns>
    public async Task<IEnumerable<PostViewModel>> GetPostsAsync(CancellationToken cancellationToken)
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
        var authorName = identityProvider.User?.GetName() ?? throw new NotAuthorizedException();
        var author = await usersRepository.GetByNameAsync(authorName, cancellationToken) ?? throw new NotAuthorizedException();
        
        var post = request.MapToPost(author.Id);
        postsRepository.Add(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}