namespace Layers.Infrastructure.DbModels.MappingExtensions;

public static class PostMapping
{
    /// <summary> Смаппить пост из домена для БД </summary>
    /// <param name="domainPost"> Модель из Domains</param>
    public static Post ToDbPost(this TestsExample.Models.Post domainPost)
    {
        return new()
        {
            Id = domainPost.Id,
            Title = domainPost.Title,
            Content = domainPost.Content,
            UserId = domainPost.UserId
        };
    }

    /// <summary> Смаппить пост из БД в пост из домена </summary>
    /// <returns> Пост из доменного слоя </returns>
    public static TestsExample.Models.Post ToDomainPost(this Post post)
    {
        return new()
        {
            Id = post.Id,
            Content = post.Content,
            Title = post.Title,
            UserId = post.UserId,
            PublicationDateTime = post.PublicationDateTime
        };
    }
}