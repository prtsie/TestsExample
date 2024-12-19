namespace Layers.Application.NeededServices.Common;

public interface IDateTimeProvider
{
    DateTime Now { get; }
}