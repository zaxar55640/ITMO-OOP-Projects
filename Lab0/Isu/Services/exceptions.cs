namespace Isu.Services;

public class IsuException : Exception
{
    public IsuException()
        : base("Isu is down")
    { }

    public IsuException(string message)
        : base(message)
    { }

    public IsuException(string message, Exception innerException)
        : base(message, innerException)
    { }
}