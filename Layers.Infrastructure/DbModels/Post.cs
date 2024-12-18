namespace Layers.Infrastructure.DbModels;

/// <summary> Пост </summary>
public class Post
{
    /// <summary> Идентификатор </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary> Заголовок </summary>
    public required string Title { get; set; }

    /// <summary> Текст поста </summary>
    public required string Content { get; set; }

    /// <summary> Идентификатор автора </summary>
    /// <remarks>
    /// Если убрать это свойство и оставить только навигационное, то нужно будет делать лишний запрос или join в БД,
    /// чтобы получить этот id, так как EF Core по умолчанию сам не добирает навигационные свойства из БД,
    /// и они имеют значение null
    /// </remarks>
    public Guid UserId { get; set; }

    /// <summary> Навигационное свойство с автором </summary>
    public User User { get; set; } = null!;

    public DateTime PublicationDateTime { get; set; }
}