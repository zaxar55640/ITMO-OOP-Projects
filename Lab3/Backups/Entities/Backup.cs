using System.Net.Sockets;

namespace Backups.Entities;

public class Backup
{
    public Backup()
    {
        RestorePoints = new List<RestorePoint>();
    }

    public List<RestorePoint> RestorePoints { get; set; }
}