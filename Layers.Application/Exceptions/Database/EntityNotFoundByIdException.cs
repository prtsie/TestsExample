using Layers.Application.Exceptions.Common;

namespace Layers.Application.Exceptions.Database;

/// <summary>
/// Сущность не найдена по id
/// </summary>
/// <typeparam name="T">Тип сущности</typeparam>
public class EntityNotFoundByIdException<T> : NotFoundException
    where T : class
{
    public EntityNotFoundByIdException(Guid id) : base($"{typeof(T).Name} с идентификатором {id} не найден.") { }
}