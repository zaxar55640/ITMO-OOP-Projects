namespace Backups.Exceptions;

public class BackupTaskDataException : Exception
{
    public BackupTaskDataException()
        : base("BackupTask couldn't be created since wrong data were/was given.")
    {
    }
}