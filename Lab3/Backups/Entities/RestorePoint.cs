namespace Backups.Entities;

public class RestorePoint
{
    private List<Storage> _storages;
    private DateTime _creationTime;

    public RestorePoint(List<Storage> storages)
    {
        _storages = storages;
        _creationTime = DateTime.Now;
    }

    public List<Storage> GetStorages() => _storages;
}