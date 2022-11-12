using Backups.Entities;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Xunit;
namespace Backups.Test;

public class BackupsTest
{
    [Fact]
    public void CreateBackupTask_CheckRestorePointAndStorage()
    {
        BackupTask backupTask = new BackupTask("Task", new InMemoryRepository(), new SplitStorage());
        BackupObject obj1 = new BackupObject("file1", "path1");
        BackupObject obj2 = new BackupObject("file2", "path2");
        backupTask.AddObject(obj1);
        backupTask.AddObject(obj2);
        backupTask.CreateRestorePoint();
        backupTask.RemoveObject(obj1);
        backupTask.CreateRestorePoint();
        IReadOnlyList<RestorePoint> restorePoints = backupTask.Backupp.RestorePoints;
        IReadOnlyList<Storage> firstStorages = restorePoints[0].Storages;
        IReadOnlyList<Storage> secondStorages = restorePoints[1].Storages;
        Assert.Equal(2, firstStorages.Count);
        Assert.Single(secondStorages);
        Assert.Equal(2, restorePoints.Count);
    }
}