using Backups.Entities;
using Backups.Extra.ClearingRPAlgorhytms;
using Backups.Extra.Exceptions;
using Backups.Extra.Logger;
using Backups.Models;
namespace Backups.Extra;

public class BackupTaskExtra
{
    private ILogger _logger;
    private BackupTask _bt;

    public BackupTaskExtra(BackupTask bt, ILogger logger)
    {
        if (logger == null) throw new BackupTaskExtraException("Logger is not active");
        if (bt == null) throw new BackupTaskExtraException("BackupTask is not existing");
        _logger = logger;
        _bt = bt;
        _logger.WriteLog("Backup Task created.");
    }

    public void AddObject(BackupObject backupObject)
    {
        _bt.AddObject(backupObject);
        _logger.WriteLog($"Object {backupObject.Name} was created.");
    }

    public void CreateRestorePoint()
    {
        _bt.CreateRestorePoint();
        _logger.WriteLog("RestorePoint created.");
        List<Storage> storages = _bt.Backupp.RestorePoints.OrderByDescending(p => p.CreationTime).First().Storages;
        storages.ForEach(s => _logger.WriteLog($"{s.Name} storage was created."));
    }

    public void RemoveObject(BackupObject backupObject)
    {
        _bt.RemoveObject(backupObject);
        _logger.WriteLog($"Object {backupObject.Name} was deleted.");
    }
}