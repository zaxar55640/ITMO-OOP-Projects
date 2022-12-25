namespace ClassLibrary1.DataAccess.Exceptions;

public class SessionException : Exception
{
    public SessionException(string message)
        : base(message) { }
}