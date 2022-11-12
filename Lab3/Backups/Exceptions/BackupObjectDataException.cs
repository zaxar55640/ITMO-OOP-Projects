namespace Backups.Exceptions;

public class BackupObjectDataException : Exception
{
    public BackupObjectDataException()
        : base("BackupObject couldn't be created since wrong data were/was given.")
    {
    }
}