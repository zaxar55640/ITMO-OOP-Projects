namespace Backups.Extra.Exceptions;

public class RestoreException : Exception
{
    public RestoreException(string message)
        : base(message) { }
}