using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    public List<Storage> CreateRepository(BackupTask backupTask)
    {
        if (!Directory.Exists($"./{backupTask.Name}"))
            Directory.CreateDirectory($"./{backupTask.Name}");
        List<Storage> storages = new List<Storage>();
        backupTask.GetObjects
            .ForEach(p =>
            {
                Storage storage = backupTask.StorageModel.CreateStorage(p, backupTask.GetRestorePoints().Count, backupTask.Name);
                if (!File.Exists(storage.Path))
                {
                    FileStream fs = File.Create(storage.Path);
                    fs.Close();
                }

                ZipArchive zip = ZipFile
                    .Open(storage.Path, File.Exists(storage.Path) ? ZipArchiveMode.Update : ZipArchiveMode.Create);
                zip.CreateEntryFromFile(p.Path, p.Name);
                zip.Dispose();
                storages.Add(storage);
            });
        return storages;
    }
}