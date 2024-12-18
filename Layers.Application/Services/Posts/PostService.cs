using Layers.Application.Exceptions.Auth;
using Layers.Application.Exceptions.Database;
using Layers.Application.Helpers.MappingExtensions;
using Layers.Application.Models;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Application.Requests;
using TestsExample.Models;

namespace Layers.Application.Services.Posts;

public class PostService : IPostService
{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly IUnitOfWork unitOfWork;

    public PostService(
        IPostRepository postRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.unitOfWork = unitOfWork;
    }

    async Task<IEnumerable<PostViewModel>> IPostService.GetPostsAsync(Sort sort, CancellationToken cancellationToken)
    {
        var posts = await postRepository.GetWithAuthorName(sort, cancellationToken);

        var models = posts.Select(tuple => tuple.Item1.MapToPostViewModel(tuple.Item2));
        
        return models;
    }

    async Task IPostService.CreatePostAsync(CreatePostRequest request, Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(userId, cancellationToken) ?? throw new NotAuthorizedException();

        var post = request.MapToPost(user.Id);
        postRepository.Add(post);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    async Task IPostService.DeletePostAsync(Guid postId, Guid userId, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(userId, cancellationToken) ?? throw new NotAuthorizedException();
        
        var post = await postRepository.GetById(postId, cancellationToken) 
                   ?? throw new EntityNotFoundByIdException<Post>(postId);
        
        if (post.UserId != user.Id)
        {
            throw new NotAuthorizedException();
        }
        
        postRepository.Delete(post);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}