using Backups.Entities;
using Backups.Models;

namespace Backups.Extra.Restore;

public interface IRestorer
{
    void Restore(RestorePoint rp, BackupTask bt, string path);
}