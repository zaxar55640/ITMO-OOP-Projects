using System.Security.Cryptography.X509Certificates;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
namespace Backups.Models;

public class BackupTask
{
    private List<BackupObject> _objects;
    public BackupTask(string name, IRepository repository, IStorageModel storageModel)
    {
        if (name == string.Empty || repository == null || storageModel == null)
            throw new BackupTaskDataException();
        Name = name;
        Backupp = new Backup();
        _objects = new List<BackupObject>();
        Repository = repository;
        StorageModel = storageModel;
    }

    public IStorageModel StorageModel { get; }
    public IRepository Repository { get; }
    public Backup Backupp { get; set; }
    public IReadOnlyList<BackupObject> Objects => _objects;
    public string Name { get; }
    public List<BackupObject> GetObjects => _objects;

    public IReadOnlyList<RestorePoint> GetRestorePoints()
    {
        IReadOnlyList<RestorePoint> rp = Backupp.RestorePoints;
        return rp;
    }

    public void AddObject(BackupObject backupObject)
    {
        if (backupObject == null)
        {
            throw new BackupException("Invalid backup object");
        }

        if (_objects.Contains(backupObject))
        {
            throw new BackupException("This backupObject is already in the backup task");
        }

        _objects.Add(backupObject);
    }

    public void RemoveObject(BackupObject backupObject)
    {
        if (backupObject == null)
        {
            throw new BackupException("Invalid backup object");
        }

        _objects.Remove(backupObject);
    }

    public void CreateRestorePoint()
    {
        List<Storage> storages = Repository.CreateRepository(this);
        RestorePoint point = new RestorePoint(storages);
        Backupp.RestorePoints.Add(point);
    }
}