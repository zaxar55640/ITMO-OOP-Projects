using System.Security.Cryptography.X509Certificates;
using Backups.Entities;
using Backups.Exceptions;
using Backups.Interfaces;
namespace Backups.Models;

public class BackupTask
{
    private List<BackupObject> _objects;
    private Backup _backup;
    private string _name;

    public BackupTask(string name, IRepository repository, IStorageModel storageModel)
    {
        if (name == string.Empty || repository == null || storageModel == null)
            throw new BackupTaskDataException();
        _name = name;
        _backup = new Backup();
        _objects = new List<BackupObject>();
        Repository = repository;
        StorageModel = storageModel;
    }

    public IStorageModel StorageModel { get; }
    public IRepository Repository { get; }
    public string GetName() => _name;
    public List<BackupObject> GetObjects() => _objects;
    public List<RestorePoint> GetRestorePoints() => _backup.GetRestorePoints();
    public void AddObject(BackupObject backupObject) => GetObjects().Add(backupObject);
    public void RemoveObject(BackupObject backupObject) => GetObjects().Remove(backupObject);
    public void CreateRestorePoint()
    {
        List<Storage> storages = Repository.CreateRepository(this);
        RestorePoint point = new RestorePoint(storages);
        _backup.GetRestorePoints().Add(point);
    }
}