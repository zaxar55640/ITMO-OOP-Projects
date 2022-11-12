using Backups.Exceptions;
namespace Backups.Entities;

public class BackupObject
{
    public BackupObject(string name, string path)
    {
        if (name == string.Empty || path == string.Empty)
            throw new BackupObjectDataException();
        Name = name;
        Path = path;
    }

    public string Name { get; }
    public string Path { get; }
}