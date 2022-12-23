namespace Backups.Extra.Exceptions;

public class BackupTaskExtraException : Exception
{
    public BackupTaskExtraException(string message)
        : base(message) { }
}