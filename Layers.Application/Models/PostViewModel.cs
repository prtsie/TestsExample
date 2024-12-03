namespace Layers.Application.Models;

/// <summary>
/// Модель поста для представления в пользовательском интерфейсе
/// </summary>
public class PostViewModel
{
    /// <summary> Идентификатор поста </summary>
    public Guid Id { get; set; }

    /// <summary> Никнейм автора поста </summary>
    public string Author { get; set; } = null!;

    /// <summary> Заголовок поста </summary>
    public string Title { get; set; } = null!;

    /// <summary> Текст поста </summary>
    public string Content { get; set; } = null!;

    /// <summary> Дата публикации поста </summary>
    public DateTime PublicationDateTime { get; set; }

    public Guid[] ImageIds { get; set; } = null!;
}