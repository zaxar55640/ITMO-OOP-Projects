using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
namespace Backups.StorageAlgorithms;

public class SplitStorage : IStorageModel
{
    public Storage CreateStorage(BackupObject backupObject, int restorePointNum, string backupTaskName)
    {
        if (backupObject == null || backupTaskName == null || backupTaskName == string.Empty)
            throw new StorageDataException();
        string fileName = $"{backupObject.GetName()}_{restorePointNum}";
        string path = $"./{backupTaskName}/{restorePointNum}/{fileName}.zip";
        Storage storage = new Storage(fileName, path);
        return storage;
    }
}