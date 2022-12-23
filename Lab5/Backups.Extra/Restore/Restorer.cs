using System.IO.Compression;
using Backups.Entities;
using Backups.Models;
namespace Backups.Extra.Restore;

public class Restorer : IRestorer
{
    public void Restore(RestorePoint rp, BackupTask bt, string path = "./")
    {
        List<Storage> storages = rp.Storages;
        storages.ForEach(s =>
        {
            ZipFile.ExtractToDirectory(s.Path, path);
        });
    }
}