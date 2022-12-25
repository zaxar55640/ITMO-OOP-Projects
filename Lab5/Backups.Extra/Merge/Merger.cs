using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;
using Backups.StorageAlgorithms;

namespace Backups.Extra.Merge;

public class Merger
{
    public void Merge(BackupTask backupTask)
    {
        if (backupTask == null)
            throw new MergeException("Merge isn't possible since backup task is not existing.");
        RestorePoint lastRP = backupTask.Backupp.RestorePoints.OrderBy(p => p.CreationTime).First();
        backupTask.Backupp.RestorePoints.Remove(lastRP);
        backupTask.Backupp.RestorePoints.ToList().ForEach(p =>
        {
            if (p.Storages.Count == 1)
                backupTask.Backupp.RestorePoints.Remove(p);
            if (p.Storages == lastRP.Storages)
                backupTask.Backupp.RestorePoints.Remove(p);
            if (p.Storages != lastRP.Storages)
            {
                p.Storages.ToList().ForEach(storage =>
                {
                    if (!lastRP.Storages.Contains(storage))
                        lastRP.Storages.Add(storage);
                });
            }
        });
        backupTask.Backupp.RestorePoints.Add(lastRP);
    }
}