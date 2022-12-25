using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;

namespace Backups.Extra.ClearingRPAlgorhytms;

public class HybridCleaner : ICleaner
{
    public HybridCleaner(DateTime time, int amount, string type)
    {
        if (amount == 0)
            throw new CleanerException("Hybrid cleaning could not be made because amount is 0.");
        if (type != "both" && type != "one")
            throw new CleanerException("Hybrid cleaning could not be made because type is wrong. Choose 'one' or ' both'.");
        Date = time;
        Amount = amount;
        Type = type;
        AmountCleaner = new AmountCleaner(Amount);
        DateCleaner = new DateCleaner(Date);
    }

    public DateTime Date { get; }
    public int Amount { get; }
    public string Type { get; set; }
    public AmountCleaner AmountCleaner { get; }
    public DateCleaner DateCleaner { get; }

    public List<RestorePoint> CleanRestorePoints(BackupTask backupTask)
    {
        List<RestorePoint> dateList = (List<RestorePoint>)backupTask.Backupp.RestorePoints
            .Except(AmountCleaner.CleanRestorePoints(backupTask));
        List<RestorePoint> amountList = (List<RestorePoint>)backupTask.Backupp.RestorePoints
            .Except(DateCleaner.CleanRestorePoints(backupTask));
        List<RestorePoint> match = new List<RestorePoint>();
        switch (Type)
        {
            case "both":
                match = (List<RestorePoint>)dateList.Intersect(amountList);
                break;
            case "one":
                match = (List<RestorePoint>)dateList.Union(amountList);
                break;
        }

        backupTask.Backupp.RestorePoints = (List<RestorePoint>)backupTask.Backupp.RestorePoints.Except(match);
        return (List<RestorePoint>)backupTask.Backupp.RestorePoints;
    }
}