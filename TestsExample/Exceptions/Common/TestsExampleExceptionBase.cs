namespace TestsExample.Exceptions.Common;

/// <summary>
/// Базовый класс исключений веб-приложения
/// </summary>
public abstract class TestsExampleExceptionBase : Exception
{
    protected TestsExampleExceptionBase(string message) : base(message) { }
}