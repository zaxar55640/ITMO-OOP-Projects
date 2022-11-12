namespace Backups.TestFiles
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            BackupTask backupTask = new BackupTask("Task", new FileSystemRepository(), new SingleStorage());
            BackupObject obj1 = new BackupObject("file1.txt", "./file1.txt");
            BackupObject obj2 = new BackupObject("file2.txt", "./file2.txt");
            backupTask.AddObject(obj1);
            backupTask.AddObject(obj2);
            backupTask.CreateRestorePoint();
            Console.WriteLine("FilesCreated");
        }
    }
}