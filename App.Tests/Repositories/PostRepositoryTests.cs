using Ahatornn.TestGenerator;
using FluentAssertions;
using Layers.Application.Models;
using Layers.Application.NeededServices.Database.Repositories;
using Layers.Infrastructure.Database.Repositories;
using Layers.Infrastructure.DbModels.MappingExtensions;
using Microsoft.EntityFrameworkCore;
using TestsExample.Models;
using Xunit;

namespace Repositories.Tests.Repositories;

/// <summary>
/// Пример тестов на один из репозиториев
/// </summary>
public class PostRepositoryTests : RepositoryTestsBase
{
    private readonly IPostRepository postRepository;

    public PostRepositoryTests()
    {
        postRepository = new PostRepository(Context, Context);
    }
    
    /// <summary>
    /// Метод GetById должен возвращать значение, если оно есть в БД
    /// </summary>
    [Fact]
    public async Task GetByIdShouldReturnValue()
    {
        // Arrange
        var post = TestEntityProvider.Shared.Create<Post>( // Создание модели поста
            p => p.Id = Guid.NewGuid()); // Лямбда-выражение, задающее нужные в данном тесте свойства модели
        var mapped = post.ToDbPost(); // Смаппить объект к модели БД
        Context.Add(mapped); // Добавляем модель в БД напрямую через контекст, а не через репозиторий
        await Context.SaveChangesAsync(); // Сохранение изменений в БД
        // На всякий случай говорим контексту, что больше не надо отслеживать изменения модели, иначе дальше может быть исключение
        Context.Entry(mapped).State = EntityState.Detached; 

        // Act
        var result = await postRepository.GetById(post.Id, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(post);
        // Метод "BeEquivalentTo" сравнивает фактическое и ожидаемое значение по названиям свойств их классов,
        // так что с помощью него можно сравнивать объекты разных классов, но с одинаковыми полями.
        // Если нужно сравнить только некоторые поля, то можно передать методу анонимный тип:
        // result.Should().BeEquivalentTo(new { Id = post.Id });
        // Метод "Be" использует сравнение по ссылке на объект в памяти, что в данном случае не сработает.
    }
    
    // Здесь должен быть ещё один тест на метод GetById,
    // чтобы покрыть все варианты его поведения — возвращение найденного значения (тест выше проверяет это)
    // и возвращение null, если ничего не нашлось

    /// <summary>
    /// Метод Get должен возвращать пустой список, если БД пуста
    /// </summary>
    [Theory] // Указываем атрибут Theory, чтобы можно было использовать атрибут InlineData,
    // который xUnit будет использовать, чтобы получить значения для параметров теста,
    // то есть тест отработает в данном случае шесть раз, по разу на каждое значение enum-а
    [InlineData(Sort.Author)]
    [InlineData(Sort.AuthorReverse)]
    [InlineData(Sort.Title)]
    [InlineData(Sort.TitleReverse)]
    [InlineData(Sort.Date)]
    [InlineData(Sort.DateReverse)]
    public async Task GetShouldReturnEmpty(Sort sort)
    {
        // Arrange не будет, потому что ничего изначально не нужно

        // Act
        var result = await postRepository.Get(sort, CancellationToken.None); // Используем параметр теста
        
        // Assert
        result.Should().BeEmpty();
    }
    
    /// <summary>
    /// Метод Get должен возвращать список постов из БД, отсортированный по дате публикации
    /// </summary>
    [Fact]
    public async Task GetShouldReturnPostsOrderedByDate()
    {
        // Arrange
        var dateTime = DateTime.Now;
        var post1 = TestEntityProvider.Shared.Create<Post>(p =>
        {
            p.Id = Guid.NewGuid();
            p.PublicationDateTime = dateTime;
        });
        var mapped1 = post1.ToDbPost();
        var post2 = TestEntityProvider.Shared.Create<Post>(p =>
        {
            p.Id = Guid.NewGuid();
            p.PublicationDateTime = dateTime.AddDays(-1);
        });
        var mapped2 = post2.ToDbPost();
        var post3 = TestEntityProvider.Shared.Create<Post>(p =>
        {
            p.Id = Guid.NewGuid();
            p.PublicationDateTime = dateTime.AddDays(-2);
        });
        var mapped3 = post3.ToDbPost();
        Context.AddRange(mapped2, mapped1, mapped3); // Добавляем вразнобой
        await Context.SaveChangesAsync();
        Context.Entry(mapped1).State = EntityState.Detached; 
        Context.Entry(mapped2).State = EntityState.Detached; 
        Context.Entry(mapped3).State = EntityState.Detached; 

        // Act
        var result = await postRepository.Get(Sort.Date, CancellationToken.None);
        
        // Assert
        result.ToArray().Should().BeInDescendingOrder(p => p.PublicationDateTime); // Сверяем порядок элементов
    }
    
    // Тесты должны быть на все варианты сортировки этого метода и на все публичные методы репозитория
}