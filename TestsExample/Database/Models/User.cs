namespace TestsExample.Database.Models;

/// <summary>
/// Пользователь
/// </summary>
public class User : EntityBase
{
    /// <summary> Имя пользователя </summary>
    public string Name { get; set; } = null!;

    /// <summary> Пароль пользователя </summary>
    public string Password { get; set; } = null!;
}