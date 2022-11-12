namespace Backups.Entities;

public class RestorePoint
{
    private DateTime _creationTime;

    public RestorePoint(List<Storage> storages)
    {
        Storages = storages;
        _creationTime = DateTime.Now;
    }

    public List<Storage> Storages { get; }
}