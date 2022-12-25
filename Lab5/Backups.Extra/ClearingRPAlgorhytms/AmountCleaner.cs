using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;

namespace Backups.Extra.ClearingRPAlgorhytms;

public class AmountCleaner : ICleaner
{
    public AmountCleaner(int amount)
    {
        if (amount == 0)
            throw new CleanerException("Amount cleaning could not be made because amount is 0.");
        Amount = amount;
    }

    public int Amount { get; set; }
    public List<RestorePoint> CleanRestorePoints(BackupTask backupTask)
    {
        if (backupTask == null)
            throw new CleanerException("Amount cleaning could not be made because backup task given is not existing.");
        backupTask.Backupp.RestorePoints = backupTask.Backupp.RestorePoints.OrderByDescending(p => p.CreationTime).Take(Amount).ToList();
        return (List<RestorePoint>)backupTask.Backupp.RestorePoints
            .OrderByDescending(p => p.CreationTime).Take(Amount).ToList();
    }
}