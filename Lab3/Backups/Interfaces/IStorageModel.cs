using Backups.Entities;
using Backups.Models;
namespace Backups.Interfaces;

public interface IStorageModel
{
    public Storage CreateStorage(BackupObject backupObject, int restorePointNum, string backupTaskName);
}