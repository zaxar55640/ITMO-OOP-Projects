using Backups.Entities;
using Backups.Models;

namespace Backups.Extra.ClearingRPAlgorhytms;

public interface ICleaner
{
    public List<RestorePoint> CleanRestorePoints(BackupTask backupTask);
}