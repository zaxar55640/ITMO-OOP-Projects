using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
namespace Backups.Repositories;

public class InMemoryRepository : IRepository
{
    public List<Storage> CreateRepository(BackupTask backupTask)
    {
        return backupTask.GetObjects()
            .Select(p => backupTask
                .StorageModel.CreateStorage(p, backupTask.GetRestorePoints().Count, backupTask.GetName())).ToList();
    }
}