namespace Backups.Entities;

public class RestorePoint
{
    public RestorePoint(List<Storage> storages)
    {
        Storages = storages;
        CreationTime = DateTime.Now;
    }

    public DateTime CreationTime { get; }
    public List<Storage> Storages { get; }
}