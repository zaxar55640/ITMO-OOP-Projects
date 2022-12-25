using dsadsa
namespace Test;

internal static class FileSystemTest
{
    public static void Main(string[] args)
    {
        BackupTask backupTask = new BackupTask("Task", new FileSystemRepository(), new SingleStorage());
        BackupObject obj1 = new BackupObject("file1.txt", "./file1.txt");
        BackupObject obj2 = new BackupObject("file5.txt", "./file5.txt");
        BackupObject obj3 = new BackupObject("file3.txt", "./file3.txt");
        backupTask.AddObject(obj1);
        backupTask.AddObject(obj2);
        backupTask.CreateRestorePoint();
        backupTask.AddObject(obj3);
        backupTask.CreateRestorePoint();
        Console.WriteLine("FilesCreated");
    }
}