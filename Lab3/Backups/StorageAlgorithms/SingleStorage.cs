﻿using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
namespace Backups.StorageAlgorithms;

public class SingleStorage : IStorageModel
{
    public Storage CreateStorage(BackupObject backupObject, int restorePointNum, string backupTaskName)
    {
        if (backupObject == null || backupTaskName == null || backupTaskName == string.Empty)
            throw new StorageDataException();
        string fileName = $"{backupObject.Name}_{restorePointNum}";
        string path = $"./{backupTaskName}/{restorePointNum}.zip";
        Storage storage = new Storage(fileName, path);
        return storage;
    }
}