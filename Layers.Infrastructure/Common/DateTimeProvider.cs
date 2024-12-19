using Layers.Application.NeededServices.Common;

namespace Layers.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now { get; } = DateTime.UtcNow;
}