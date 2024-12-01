namespace TestsExample.Database.Models;

public abstract class EntityBase
{
    /// <summary> Идентификатор пользователя </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
}