namespace Backups.Extra.Exceptions;

public class CleanerException : Exception
{
    public CleanerException(string message)
        : base(message) { }
}