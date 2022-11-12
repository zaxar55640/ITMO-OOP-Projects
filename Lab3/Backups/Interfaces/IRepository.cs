using Backups.Entities;
using Backups.Models;
namespace Backups.Interfaces;

public interface IRepository
{
    public List<Storage> CreateRepository(BackupTask backupTask);
}