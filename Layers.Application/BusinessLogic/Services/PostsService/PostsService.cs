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
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    public PostsService(
        IPostRepository postRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    async Task<IEnumerable<PostViewModel>> IPostsService.GetPostsAsync(CancellationToken cancellationToken)
    {
        var posts = await postRepository.GetAllAsync(cancellationToken);

        var models = posts.Select(p =>
        {
            var author = userRepository.GetByIdAsync(p.UserId, cancellationToken).Result;

            return p.MapToPostViewModel(
                author!.Name //TODO: обработка null
                );
        });
        
        return models;
    }

    async Task IPostsService.CreatePostAsync(CreatePostRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new NotAuthorizedException();

        var post = request.MapToPost(user.Id);
        postRepository.Add(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IPostsService.DeletePostAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(userId, cancellationToken) ?? throw new NotAuthorizedException();
        
        var post = await postRepository.GetByIdAsync(postId, cancellationToken) 
                   ?? throw new EntityNotFoundByIdException<Post>(postId);
        
        if (post.UserId != user.Id)
        {
            throw new NotAuthorizedException();
        }
        
        postRepository.Remove(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}