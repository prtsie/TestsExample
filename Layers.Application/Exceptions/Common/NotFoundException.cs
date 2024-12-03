namespace Layers.Application.Exceptions.Common;

public abstract class NotFoundException : TestsExampleExceptionBase
{
    protected NotFoundException(string message) : base(message)
    {
    }
}