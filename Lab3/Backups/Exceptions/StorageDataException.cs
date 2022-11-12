namespace Backups.Exceptions;

public class StorageDataException : Exception
{
    public StorageDataException()
        : base("Storage couldn't be created since wrong data were/was given.")
    {
    }
}