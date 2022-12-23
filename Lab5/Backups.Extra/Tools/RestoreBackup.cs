using System.IO.Compression;
using System.Text.Json;
using Backups.Entities;
using Backups.Extra.Exceptions;
using Backups.Models;
namespace Backups.Extra.Tools;

public class RestoreBackup
{
    public RestoreBackup(string path, BackupTask bt)
    {
        if (path == string.Empty) throw new RestoreException("The path is not existing");
        if (bt == null) throw new RestoreException("Backup Task is not existing");
        Path = path;
        BBackupTask = bt;
    }

    private string Path { get; set; }
    private BackupTask BBackupTask { get; set; }

    public BackupTask Restore()
    {
        BackupTask bt = JsonSerializer.Deserialize<BackupTask>(File.ReadAllText(Path)) ?? throw new InvalidOperationException();
        return bt;
    }

    public void Save()
    {
        if (File.Exists(Path))
        {
            File.Delete(Path);
        }

        File.AppendAllText(Path, JsonSerializer.Serialize(BBackupTask));
    }
}