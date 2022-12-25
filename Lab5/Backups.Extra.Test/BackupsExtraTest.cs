using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Backups.Entities;
using Backups.Extra;
using Backups.Extra.ClearingRPAlgorhytms;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Extra.Tools;
using Backups.Models;
using Backups.Repositories;
using Backups.StorageAlgorithms;
using Xunit;
namespace Backups.Extra.Test;

public class BackupsExtraTest
{
    [Fact]
    public void CleanRestoreMerge_RestorePointAltered()
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
        Assert.Equal(2, restorePoints.Count);
        AmountCleaner cl = new AmountCleaner(1);
        cl.CleanRestorePoints(backupTask);
        Assert.Single(backupTask.Backupp.RestorePoints);
        backupTask.CreateRestorePoint();
        new Merger().Merge(backupTask);
        Assert.Single(backupTask.Backupp.RestorePoints);
    }
}