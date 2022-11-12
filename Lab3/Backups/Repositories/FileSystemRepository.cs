using System.IO.Compression;
using Backups.Entities;
using Backups.Interfaces;
using Backups.Models;
namespace Backups.Repositories;

public class FileSystemRepository : IRepository
{
    public List<Storage> CreateRepository(BackupTask backupTask)
    {
        if (!Directory.Exists($"./{backupTask.GetName()}"))
            Directory.CreateDirectory($"./{backupTask.GetName()}");
        List<Storage> storages = new List<Storage>();
        backupTask.GetObjects()
            .ForEach(p =>
            {
                Storage storage = backupTask.StorageModel.CreateStorage(p, backupTask.GetRestorePoints().Count, backupTask.GetName());
                if (!File.Exists(storage.GetPath()))
                {
                    FileStream fs = File.Create(storage.GetPath());
                    fs.Close();
                }

                ZipArchive zip = ZipFile
                    .Open(storage.GetPath(), File.Exists(storage.GetPath()) ? ZipArchiveMode.Update : ZipArchiveMode.Create);
                zip.CreateEntryFromFile(p.GetPath(), p.GetName());
                zip.Dispose();
                storages.Add(storage);
            });
        return storages;
    }
}