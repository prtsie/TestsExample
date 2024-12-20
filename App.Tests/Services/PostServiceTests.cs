using Ahatornn.TestGenerator;
using FluentAssertions;
using Layers.Application.Exceptions.Auth;
using Layers.Application.Models;
using Layers.Application.NeededServices.Database;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Application.Requests;
using Layers.Application.Services.Posts;
using Layers.Infrastructure.Common;
using Moq;
using TestsExample.Models;
using Xunit;

namespace Repositories.Tests.Services;

public class PostServiceTests
{
    /// <summary>
    /// Создание поста должно бросать исключение для несуществующего пользователя
    /// </summary>
    [Fact]
    public async Task CreatePostShouldThrowForNonExistingUser()
    {
        var userRepositoryStub = new Mock<IUserRepository>(); // Создаём заглушку
        userRepositoryStub
            // Можно вызывать метод GetById с любыми параметрами
            .Setup(r => r.GetById(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            // И он всегда вернёт null
            .ReturnsAsync(() => null);
        var postRepository = Mock.Of<IPostRepository>(); // Сразу получаем объект заглушки, не настраивая её,
                                                             // потому что её поведение в данном случае не важно 
        var unitOfWork = Mock.Of<IUnitOfWork>();
        IPostService service = new PostService( // Если не указать интерфейс вместо типа сервиса,
                                                // то не будут доступны его методы интерфейса,
                                                // если они реализованы явно (с названием интерфейса перед именем метода)
            postRepository,
            userRepositoryStub.Object, // Получаем объект заглушки
            unitOfWork,
            new DateTimeProvider()); // Здесь нет причин не использовать настоящую реализацию, так что я использую её
        var request = TestEntityProvider.Shared.Create<CreatePostRequest>();
        
        // Если мы хотим проверить, бросает ли какой-то метод исключение,
        // мы можем записать в переменную лямбда-выражение,
        // вызывающее нужный метод с нужными параметрами,
        // и ассертами проверить нужное нам исключение
        var act = () => service.CreatePostAsync(request, Guid.NewGuid(), CancellationToken.None);

        // Проверяем, что метод бросает то исключение, которое мы ожидаем.
        // Метод будет выполнен только на этом этапе, а не раньше
        await act.Should().ThrowAsync<NotAuthorizedException>();
    }
    
    // Здесь ещё как минимум один тест на CreatePost
    
    /// <summary>
    /// Метод GetPosts должен возвращать посты
    /// </summary>
    [Theory]
    [InlineData(Sort.Author)]
    [InlineData(Sort.AuthorReverse)]
    [InlineData(Sort.Title)]
    [InlineData(Sort.TitleReverse)]
    [InlineData(Sort.Date)]
    [InlineData(Sort.DateReverse)]
    public async Task GetPostsShouldReturnPosts(Sort sort)
    {
        var posts = new[]
        {
            TestEntityProvider.Shared.Create<Post>(),
            TestEntityProvider.Shared.Create<Post>(),
            TestEntityProvider.Shared.Create<Post>()
        };
        var userRepository = Mock.Of<IUserRepository>();
        var postRepositoryStub = new Mock<IPostRepository>();
        postRepositoryStub
            .Setup(r => r.GetWithAuthorName(It.IsAny<Sort>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => Enumerable.Range(0, 3).Select(i => new Tuple<Post, string>(posts[i], i.ToString())));
        var unitOfWork = Mock.Of<IUnitOfWork>();
        IPostService service = new PostService(postRepositoryStub.Object, userRepository, unitOfWork, new DateTimeProvider());
        
        // Если мы хотим проверить, бросает ли какой-то метод исключение,
        // мы можем записать в переменную лямбда-выражение,
        // вызывающее нужный метод с нужными параметрами,
        // и ассертами проверить нужное нам исключение
        var result = await service.GetPostsAsync(sort, CancellationToken.None);
        
        // Сортировка не важна, потому что ей занимается не сервис (хотя кто-то, может, и здесь бы написал тесты на сортировку ¯\_(ツ)_/¯)
        result.Should().BeEquivalentTo(posts.Select(p => new { Id = p.Id }));
    }
    
    // Здесь ещё тесты.
    // Можно использовать настоящую реализацию репозиториев, если хочется, всё равно у всех разный взгляд на то, где нужно использовать моки.
    // Но для этого нужно перенести и переименовать базовый класс, который предоставляет контекст БД в памяти
}