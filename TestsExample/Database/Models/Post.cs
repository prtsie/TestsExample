namespace TestsExample.Database.Models;

/// <summary> Пост </summary>
public class Post : EntityBase
{
    /// <summary> Заголовок </summary>
    public string Title { get; set; } = null!;

    /// <summary> Текст поста </summary>
    public string Content { get; set; } = null!;

    /// <summary> Идентификатор автора </summary>
    public Guid UserId { get; set; }

    public DateTime PublicationDateTime { get; set; }
}