using TestsExample.Database.Models;
using TestsExample.Exceptions.Common;

namespace TestsExample.Database.Exceptions;

/// <summary>
/// Сущность не найдена по id
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public class EntityNotFoundByIdException<T> : NotFoundException
    where T : EntityBase
{
    public EntityNotFoundByIdException(Guid id) : base($"{typeof(T).Name} с идентификатором {id} не найден.") { }
}