namespace Layers.Infrastructure.DbModels;

/// <summary>
/// Пользователь
/// </summary>
public class User
{
    /// <summary> Идентификатор </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary> Имя пользователя </summary>
    public required string Name { get; set; }

    /// <summary> Пароль пользователя </summary>
    public required string Password { get; set; }

    /// <summary> Навигационное свойство с постами </summary>
    public IEnumerable<Post> Posts { get; set; } = null!;
}