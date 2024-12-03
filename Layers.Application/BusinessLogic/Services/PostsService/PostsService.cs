using Layers.Application.Exceptions.Auth;
using Layers.Application.Exceptions.Database;
using Layers.Application.Helpers.MappingExtensions;
using Layers.Application.Models;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Application.Requests;
using TestsExample.Models;

namespace Layers.Application.BusinessLogic.Services.PostsService;

public class PostsService : IPostsService
{
    private readonly IPostsRepository postsRepository;
    private readonly IUsersRepository usersRepository;
    private readonly IUnitOfWork unitOfWork;

    public PostsService(
        IPostsRepository postsRepository,
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        this.postsRepository = postsRepository;
        this.usersRepository = usersRepository;
        this.unitOfWork = unitOfWork;
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

    async Task IPostsService.CreatePostAsync(CreatePostRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(userId, cancellationToken) ?? throw new NotAuthorizedException();

        var post = request.MapToPost(user.Id);
        postsRepository.Add(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IPostsService.DeletePostAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetByIdAsync(userId, cancellationToken) ?? throw new NotAuthorizedException();
        
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