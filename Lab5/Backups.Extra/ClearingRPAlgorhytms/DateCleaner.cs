using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;

namespace Backups.Extra.ClearingRPAlgorhytms;

public class DateCleaner : ICleaner
{
    public DateCleaner(DateTime time)
    {
        Date = time;
    }

    private DateTime Date { get; set; }
    public List<RestorePoint> CleanRestorePoints(BackupTask backupTask)
    {
        if (backupTask == null)
            throw new CleanerException("Date cleaning could not be made because backup task given is not existing.");
        backupTask.Backupp.RestorePoints.RemoveAll(p => p.CreationTime < Date);
        return backupTask.Backupp.RestorePoints;
    }
}