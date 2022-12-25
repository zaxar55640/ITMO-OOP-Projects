namespace Backups.Extra.Exceptions;

public class MergeException : Exception
{
    public MergeException(string message)
        : base(message) { }
}